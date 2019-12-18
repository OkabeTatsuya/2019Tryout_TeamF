using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBezier : MonoBehaviour
{
    private Vector3 _firstPoint = Vector3.zero;
    private Vector3 _endPoint = Vector3.zero;
    [SerializeField, Tooltip("分割するポイント")]
    private int _positionCount = 2;
    [SerializeField, Tooltip("まくの伸びる位置")]
    private Vector3 _centerPoint;
    [SerializeField, Tooltip("反発速度")]
    private float _speed = 1.0f;
    [SerializeField, Header("Debug")]
    private bool _debug = false;
    [SerializeField, Tooltip("確認用オブジェクト")]
    private Transform _pointObj = null;
    // コルーチンの間隔
    private float _SecondTimeHit = 0;
    // コルーチンの保存
    private Coroutine _coroutine;
    // LineRenderer取得用変数
    private LineRenderer _lineRenderer;
    private float _angle;

    // Start is called before the first frame update
    void Start()
    {
        _centerPoint.y = -2;
        _lineRenderer = GetComponent<LineRenderer>();
        if(_lineRenderer != null)
        {
            _lineRenderer.positionCount = _positionCount;
        }
        _lineRenderer.startWidth = 0.5f;
        _lineRenderer.endWidth = 0.5f;
        // 初期化完了後
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _centerPoint.y = -2;
    }

    // ベジェ曲線で対象のオブジェクトを曲げる
    public Vector3 SetCurve(Vector3 first, Vector3 end)
    {
        _firstPoint = first;
        _endPoint = end;
        Debug.Log("座標の計算を開始します。");
        if(_positionCount < 2)
        {
            return Vector3.zero;
        }
        
        if (_lineRenderer != null)
        {
            _lineRenderer.positionCount = _positionCount;
        }
        Debug.Log("処理を始めます");
        // 現在の始点と終点の間から動いた距離
        Vector3 center = ((first + end) / 2) + _centerPoint;
        // 始点の座標をセット
        _lineRenderer.SetPosition(0, first);
        // 終点の座標をセット
        _lineRenderer.SetPosition(_positionCount - 1, end);
        // 中間地点の座標をセット
        for(int i = 0; i < _positionCount; i++)
        {
            float t = (float)i / (_positionCount - 1);
            //Debug.Log(t);
            Vector3 pointBezier = BezierCurve(first, end, center, t);
            _lineRenderer.SetPosition(i, pointBezier);
        }
        return Vector3.zero;
    }

    private void HitCenter(Vector3 vector, float Power)
    {
        // ベクトルと球の速度を取得して、センターの位置をへこませる。
    }

    // ベジェ曲線の計算式
    public Vector3 BezierCurve(Vector3 startPoint, Vector3 endPoint, Vector3 center, float t)
    {
        // ポイント1、ポイント2、ポイント3からなる2次元のベジェ曲線
        Vector3 _point1 = Vector3.Lerp(startPoint, center, t);
        Vector3 _point2 = Vector3.Lerp(center, endPoint, t);
        Vector3 _point3 = Vector3.Lerp(_point1, _point2, t);
        // 
        if(_debug && _pointObj != null)
        {
            _pointObj.position = _point3;
        }
        return _point3;
    }
    public void Hit()
    {
        _coroutine = StartCoroutine(PrototypeHit());
    }

    public IEnumerator PrototypeHit()
    {
        while(_SecondTimeHit < 1.0f)
        {
            _SecondTimeHit += Time.deltaTime;
            _centerPoint = new Vector3(0, Mathf.Sin(Time.time * _speed), 0);
            yield return null;
        }
        _SecondTimeHit = 0f;
        _coroutine = null;
        gameObject.SetActive(false);
    }

    public IEnumerator Adjustment()
    {
        Debug.Log("コルーチンを開始する");
        while (_centerPoint.magnitude >= 0.1)
        {
            Debug.Log("コルーチンのループ");
            _centerPoint = Vector3.Lerp(_centerPoint, Vector3.zero, Time.deltaTime * _speed);
            SetCurve(_firstPoint, _endPoint);
            yield return null;
        }
        _centerPoint = Vector3.zero;
        Debug.Log("コルーチンの終了");
    }

    private void Center()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // SetCurve(_firstPoint,_endPoint);
    }
}
