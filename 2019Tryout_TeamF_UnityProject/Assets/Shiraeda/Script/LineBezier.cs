using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBezier : MonoBehaviour
{
    // 始点座標
    private Vector3 _firstPoint = Vector3.zero;
    // 終点座標
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
    [SerializeField, Tooltip("ボール")]
    private GameObject _ball = null;

    // コルーチンの間隔
    private float _SecondTimeHit = 0;
    // コルーチンの保存
    private Coroutine _coroutine;
    // LineRenderer取得用変数
    private LineRenderer _lineRenderer;
    // ラインの角度
    private float _angle;
    // ラインの向き
    private Vector2 _dir;
    private Rigidbody2D _ballRigidbody;
    private Vector2 _boundDir;
    private Vector2 _hitPoint;
    // デバック用のラインレンダラー表示
    [SerializeField, Header("Debug")]
    private PointLine _Line;
    [SerializeField]
    private PointLine _ballLine;
    [SerializeField]
    private PointLine _BoundLine;
    public float Angle
    {
        set { _angle = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _coroutine = null;
        _centerPoint.y = -2;
        _lineRenderer = GetComponent<LineRenderer>();
        if(_lineRenderer != null)
        {
            _lineRenderer.positionCount = _positionCount;
        }
        _lineRenderer.startWidth = 0.5f;
        _lineRenderer.endWidth = 0.5f;
        _ballRigidbody = _ball.GetComponent<Rigidbody2D>();
        // 初期化完了後
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _firstPoint = Vector3.zero;
        _endPoint = Vector3.zero;

        _centerPoint.y = -2;
    }

    // ベジェ曲線で対象のオブジェクトを曲げる
    public Vector3 SetCurve(Vector3 first, Vector3 end)
    {
        _firstPoint = first;
        _endPoint = end;
        if(_positionCount < 2)
        {
            return Vector3.zero;
        }   
        if (_lineRenderer != null)
        {
            _lineRenderer.positionCount = _positionCount;
        }
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


    public void StartCurve()
    {
        _centerPoint.y = -2;
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
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(PrototypeHit());
        }
    }
    public void HitCheck(Vector2 hitpos, Vector2 vector, float force)
    {
        //現在のlineの中心座標を取得
        Vector2 center = ((_firstPoint + _endPoint) / 2);

        _hitPoint = hitpos - center;
        _boundDir = (_dir + vector / 10);
        _Line.SetPointLine(0, (_firstPoint + _endPoint) / 2);
        _Line.SetPointLine(1, (_firstPoint + _endPoint) / 2 + new Vector3(_dir.x, _dir.y, 0));
        _ballLine.SetPointLine(0, hitpos);
        _ballLine.SetPointLine(1, hitpos - vector / 10);
        _BoundLine.SetPointLine(0, hitpos);
        _BoundLine.SetPointLine(1, hitpos - _boundDir);

        //_centerPoint = _hitPoint + _boundDir * 10;
        float rad = Mathf.Atan2(vector.y, vector.x);
        Debug.Log(rad * Mathf.Rad2Deg + "衝突したオブジェクトの入射角");
        // ボールの力が一定以上
        if(force > 5)
        {
        }
        // ヒット位置
        Debug.Log(hitpos);
        // Lineの中心座標
        Debug.Log(((_firstPoint + _endPoint) / 2));
    }

    public IEnumerator PrototypeHit()
    {
        while(_SecondTimeHit < 1.0f)
        {
            _SecondTimeHit += Time.deltaTime;
            _centerPoint = new Vector3(0, Mathf.Sin(Time.time * _speed), 0);
            SetCurve(_firstPoint, _endPoint);
            yield return null;
        }
        SetCurve(Vector3.zero, Vector3.zero);
        _SecondTimeHit = 0f;
        _coroutine = null;
        gameObject.SetActive(false);
    }

    public IEnumerator Extend()
    {
        _ballRigidbody.bodyType = RigidbodyType2D.Kinematic;
        _ballRigidbody.velocity = Vector2.zero;
        Debug.Log("伸びる直前");
        _centerPoint = Vector2.zero;
        while ((_hitPoint + _boundDir * 10).magnitude - _centerPoint.magnitude > 0.5)
        {
            Debug.Log("伸びます");
            _centerPoint = Vector3.Lerp(_centerPoint, _hitPoint + _boundDir * 10, Time.deltaTime * _speed);
            SetCurve(_firstPoint, _endPoint);
            yield return null;
        }
        Debug.Log("伸ばし終わり");
        _ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
        _ballRigidbody.velocity = -_boundDir * 20;
        StartCoroutine(PrototypeHit());
        yield return null;
    }

    // 値の補完
    public IEnumerator Adjustment()
    {
        while (_centerPoint.magnitude >= 0.1)
        {
            _centerPoint = Vector3.Lerp(_centerPoint, Vector3.zero, Time.deltaTime * _speed);
            SetCurve(_firstPoint, _endPoint);
            yield return null;
        }
        _centerPoint = Vector3.zero;
    }

    public void AngleMoveDown(Vector3 dir)
    {
        _dir = dir;
        Debug.Log(_dir);
    }

    // Update is called once per frame
    void Update()
    {
        SetCurve(_firstPoint, _endPoint);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_ball == null)
        {
            return;
        }
        // 衝突したオブジェクトがボールか
        if(collision.transform.tag == "Ball")
        {
            foreach(ContactPoint2D point in collision.contacts)
            {
                //_centerPoint = point.point;
            }
        }
    }
}
