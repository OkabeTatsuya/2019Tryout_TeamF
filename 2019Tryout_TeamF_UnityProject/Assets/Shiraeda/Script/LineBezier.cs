using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBezier : MonoBehaviour
{
    public enum LINE_TYPE
    {
        NON,    // 待機状態
        START,  // まくを張る処理
        EXTEND, // 伸ばす処理
        END,    // 膜が伸びる処理
        MAX
    }

    private LINE_TYPE _type;
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
    [SerializeField, Tooltip("")]
    private float _sinSpeed = 1.0f;
    [SerializeField, Tooltip("ボール")]
    private GameObject _ball = null;
    // 反射できる力
    [SerializeField, Tooltip("加わる力")]
    private float _force = 20;
    [SerializeField, Tooltip("へこむ深さ")]
    private float _dent = 10;

    // LineRenderer取得用変数
    private LineRenderer _lineRenderer;

    // ラインの向き
    private Vector2 _dir;
    private Rigidbody2D _ballRigidbody;
    private Vector2 _boundDir;
    // ヒットした座標
    private Vector2 _hitPoint;

    private Vector2 _ballSize;
    private Vector2 _vertexPosition;
    private Vector3 _centerReversePos;
    private float _nowTime = 0;
    [SerializeField]
    private float _secondTime = 1.0f;
    private float _sin = 0f;
    private Vector3 _point;
    [SerializeField]
    private LayerMask _layer = 0;
    [SerializeField]
    private GameObject[] _falseObj;
    private Vector3 vec;

	[SerializeField, Tooltip("デバック"), Header("Debug")]
    private bool _active = false;
    // デバック用のラインレンダラー表示
    [SerializeField]
    private PointLine _line;
    [SerializeField]
    private PointLine _ballLine;
    [SerializeField]
    private PointLine _boundLine;
    // Start is called before the first frame update
    void Start()
    {
		_lineRenderer = GetComponent<LineRenderer>();

        _point = Vector3.zero;
        _sin = 0;
        _type = LINE_TYPE.NON;
        _firstPoint = Vector3.zero;
        _endPoint = Vector3.zero;

        _centerPoint.y = -2;
        if (_lineRenderer != null)
        {
            _lineRenderer.positionCount = _positionCount;
        }

        _lineRenderer.startWidth = 0.5f;
        _lineRenderer.endWidth = 0.5f;
        _ballRigidbody = _ball.GetComponent<Rigidbody2D>();
        // 初期化完了後
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _type = LINE_TYPE.NON;
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
        if (_positionCount < 2)
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
        for (int i = 0; i < _positionCount; i++)
        {
            float t = (float)i / (_positionCount - 1);
            //Debug.Log(t);
            Vector3 pointBezier = BezierCurve(first, end, center, t);
            _lineRenderer.SetPosition(i, pointBezier);
        }
        _vertexPosition = _lineRenderer.GetPosition(_positionCount / 2);
        return Vector3.zero;
    }

    public void SetPoints(Vector3[] points)
    {
        _firstPoint = points[0];
        _endPoint = points[1];
        _centerPoint.y = -2;
    }

    public void SetType(LINE_TYPE type)
    {
        _type = type;
    }

    public LINE_TYPE GetLineType()
    {
        return _type;
    }

    public void StartCurve()
    {
        _centerPoint.y = -2;
    }

    public void Stop()
    {
        if (_ballRigidbody.bodyType == RigidbodyType2D.Kinematic)
        {
            _ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    // ベジェ曲線の計算式
    public Vector3 BezierCurve(Vector3 startPoint, Vector3 endPoint, Vector3 center, float t)
    {
        // ポイント1、ポイント2、ポイント3からなる2次元のベジェ曲線
        Vector3 _point1 = Vector3.Lerp(startPoint, center, t);
        Vector3 _point2 = Vector3.Lerp(center, endPoint, t);
        Vector3 _point3 = Vector3.Lerp(_point1, _point2, t);
        return _point3;
    }

    public void HitCheck(Vector2 hitPoint, Vector2 vector, float force)
    {
        vec = vector;
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

    public void EndMove()
    {
        _nowTime += Time.deltaTime;
        _sin = Mathf.Sin(Time.time * _sinSpeed);
        Vector3 point = _point * _sin * _dent;
        _centerPoint = point;
        if(_nowTime > _secondTime)
        {
            _nowTime = 0;
            _sin = 0;
            SetType(LINE_TYPE.NON);
            foreach (var obj in _falseObj)
            {
                obj.SetActive(false);
            }
        }
    }

    public float CheckRay()
    {
        var col = Physics2D.Raycast((_firstPoint + _endPoint) / 2, vec.normalized, _dent, _layer);
        if (col)
        {
            return col.distance;
        }

        return _dent;
    }

    public void Extend()
    {
        float dent = CheckRay();

        _ballRigidbody.bodyType = RigidbodyType2D.Kinematic;
        _ballRigidbody.velocity = Vector3.zero;
        _ball.transform.position = Vector3.Lerp(_ball.transform.position, _vertexPosition - (_boundDir * _ballSize.x / 2), Time.deltaTime * _speed); ;
        _centerPoint = Vector3.Lerp(_centerPoint, _hitPoint + _boundDir * dent, Time.deltaTime * _speed);
        // Debug.Log("値のチェック" + (_centerPoint.magnitude - (_hitPoint + _boundDir * _dent).magnitude));
        if ((_hitPoint + _boundDir * dent).magnitude - _centerPoint.magnitude <= 0.01f)
        {
            _centerReversePos = -_centerPoint;
            _point = (_centerPoint.normalized);

            _ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
            _ballRigidbody.velocity = _centerReversePos * _force;
            SetType(LINE_TYPE.END);
        }
    }

    // 「まく」張りのコード
    public void Adjustment()
    {
        // 線が膜を張っていない状態ならば
        if (_centerPoint.magnitude >= 0.1f)
        {
            _centerPoint = Vector3.Lerp(_centerPoint, Vector3.zero, Time.deltaTime * _speed);
        }
        else
        {
            _type = LINE_TYPE.NON;
            _centerPoint = Vector3.zero;
        }
        SetCurve(_firstPoint, _endPoint);
    }

    public void AngleMoveDown(Vector3 dir)
    {
        _dir = dir;
    }

    // Update is called once per frame
    void Update()
    {
        SetCurve(_firstPoint, _endPoint);
        // ボールの物理挙動を停止させる
        switch (_type)
        {
            case LINE_TYPE.NON:
                break;
            case LINE_TYPE.START:
                Adjustment();
                break;
            case LINE_TYPE.EXTEND:
                Extend();
                _falseObj[1].SetActive(false);
                break;
            case LINE_TYPE.END:
                _falseObj[1].SetActive(false);
                EndMove();
                break;
            case LINE_TYPE.MAX:
                break;
            default:
                break;
        }
        // 線を曲げる
        SetCurve(_firstPoint, _endPoint);
    }

    private void OnDrawGizmos()
    {
        var col = Physics2D.Raycast((_firstPoint + _endPoint) / 2, vec.normalized, _dent, _layer);

        if (col)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay((_firstPoint + _endPoint) / 2, vec * col.distance);
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay((_firstPoint + _endPoint) / 2, vec.normalized * _dent);
        }
    }
}
