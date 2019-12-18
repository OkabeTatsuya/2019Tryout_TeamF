using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBezier : MonoBehaviour
{
    private Vector3 _firstObj = Vector3.zero;
    private Vector3 _endObj = Vector3.zero;
    [SerializeField, Tooltip("中間地点")]
    private GameObject _centerObj = null;
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
    private float _SecondTimeHit = 0;

    private Coroutine _coroutine;
    // LineRenderer取得用変数
    private LineRenderer _lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
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
        _centerPoint = Vector3.zero;
    }

    public Vector3 SetCurve(Vector3 first, Vector3 end)
    {
        _firstObj = first;
        _endObj = end;
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

    public Vector3 BezierCurve(Vector3 startPoint, Vector3 endPoint, Vector3 center, float t)
    {
        // ベジェ曲線
        Vector3 _point1 = Vector3.Lerp(startPoint, center, t);
        Vector3 _point2 = Vector3.Lerp(center, endPoint, t);
        Vector3 _point3 = Vector3.Lerp(_point1, _point2, t);
        // Debug.Log(_point3);
        if(_debug)
        {
            _pointObj.position = _point3;
        }
        _centerObj.transform.position = center;
        return _point3;
    }
    public void Hit()
    {
        _coroutine = StartCoroutine(Move());
    }

    public IEnumerator Move()
    {
        Debug.Log("コルーチン開始");
        while(_SecondTimeHit < 3.0f)
        {
            Debug.Log("ループ処理");
            _SecondTimeHit += Time.deltaTime;
            _centerPoint = new Vector3(0, Mathf.Sin(Time.time * _speed), 0);
            yield return null;
        }
        _SecondTimeHit = 0f;
        _coroutine = null;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (_coroutine == null)
        //{
        //    _centerPoint = new Vector3(0, Mathf.Sin(Time.time), 0);
        //}

        SetCurve(_firstObj,_endObj);
    }
}
