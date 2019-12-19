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
    [SerializeField, Tooltip("デバック"), Header("Debug")]
    private bool _active = false;
    // デバック用のラインレンダラー表示
    [SerializeField, Header("Debug")]
    private PointLine _line;
    [SerializeField]
    private PointLine _ballLine;
    [SerializeField]
    private PointLine _boundLine;
    [SerializeField, Tooltip("確認用オブジェクト")]
    private Transform _pointObj = null;
    [SerializeField, Tooltip("力")]
    private float _force = 10;
    [SerializeField]
    private float _dent = 10;
    private Renderer _renderer;
    private Vector2 _ballSize;
    private Vector2 _vertexPosition;

    public float Angle
    {
        set { _angle = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _renderer = _ball.GetComponent<Renderer>();
        if(_renderer != null)
        {
            _ballSize = _renderer.bounds.size;
        }
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
        _vertexPosition = _lineRenderer.GetPosition(_positionCount / 2);
        return Vector3.zero;
    }


    public void StartCurve()
    {
        _centerPoint.y = -2;
    }

    // ベジェ曲線の計算式
    public Vector3 BezierCurve(Vector3 startPoint, Vector3 endPoint, Vector3 center, float t)
    {
        // ポイント1、ポイント2、ポイント3からなる2次元のベジェ曲線
        Vector3 _point1 = Vector3.Lerp(startPoint, center, t);
        Vector3 _point2 = Vector3.Lerp(center, endPoint, t);
        Vector3 _point3 = Vector3.Lerp(_point1, _point2, t);
        // 
        if(_active && _pointObj != null)
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
    public void HitCheck(Vector2 hitPoint, Vector2 vector, float force)
    {
        //現在のlineの中心座標を取得
        Vector2 center = ((_firstPoint + _endPoint) / 2);

        _hitPoint = hitPoint - center;
        _boundDir = ((_dir + vector)).normalized;

        Debug.Log(_dir.normalized);
        Debug.Log(vector.normalized);
        //_centerPoint = _hitPoint + _boundDir * 10;
        float rad = Mathf.Atan2(vector.y, vector.x);

        if (_active)
        {
            Debug.Log(_dir + "線の向き");
            Debug.Log(_boundDir + "反射角");
            Debug.Log(vector + "入射角");
            _line.SetPointLine(0, (_firstPoint + _endPoint) / 2);
            _line.SetPointLine(1, (_firstPoint + _endPoint) / 2 + new Vector3(_dir.x, _dir.y, 0));
            _ballLine.SetPointLine(0, hitPoint);
            _ballLine.SetPointLine(1, hitPoint - vector);
            _boundLine.SetPointLine(0, hitPoint);
            _boundLine.SetPointLine(1, hitPoint - _boundDir * 10);
        }
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
        // ボールの物理挙動を停止
        _ballRigidbody.bodyType = RigidbodyType2D.Kinematic;
        // 加速度停止
        _ballRigidbody.velocity = Vector2.zero;
        Debug.Log("伸びる直前");
        _centerPoint = Vector2.zero;
        while (_SecondTimeHit < 1.0f)
        {
            // 曲線の移動
            SetCurve(_firstPoint, _endPoint);

            _SecondTimeHit += Time.deltaTime;
            //Debug.Log((_hitPoint + _boundDir * _force).magnitude - _centerPoint.magnitude);
            Debug.Log("伸びます");
            Vector3 moveing_distance = Vector3.Lerp(_centerPoint, _hitPoint + _boundDir * _dent, Time.deltaTime * _speed);
            _ball.transform.position = _vertexPosition - _boundDir * _ballSize.x;
            _centerPoint = moveing_distance;

            _centerPoint = Vector3.Lerp(_centerPoint, _hitPoint + _boundDir * _dent, Time.deltaTime * _speed);
            yield return null;
        }
        _SecondTimeHit = 0;
        Debug.Log("伸ばし終わり");
        _ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
        _ballRigidbody.velocity = -_boundDir * _force;
        //AudioManager.Instance.PlaySE(AudioManager.SEClipName.)
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
