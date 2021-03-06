﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBezier : Bezier
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

    // ラインの向き
    private Vector2 _dir;
    private Rigidbody2D _ballRigidbody;
    private Vector2 _boundDir;
    // ヒットした座標
    private Vector2 _hitPoint;

    private Vector2 _ballSize;
    private Vector3 _centerReversePos;
    private float _nowTime = 0;
    [SerializeField]
    private float _secondTime = 1.0f;
    private float _sin = 0f;
    private Vector3 _pos;
    [SerializeField]
    private LayerMask _layer = 0;
    [SerializeField]
    private GameObject[] _falseObj;
    private Vector3 vec;
    private float _alpha = 1.0f;


    [Header("Debug")]
	[SerializeField, Tooltip("デバックフラグ")]
    private bool _debug = false;
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
        _alpha = 1.0f;
        _pos = Vector3.zero;
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

    public void HitCheck(Vector2 hitPoint, Vector2 vector, float force)
    {
        vec = vector;


        //現在のlineの中心座標を取得
        Vector2 center = ((_firstPoint + _endPoint) / 2);
        // 衝突地点
        _hitPoint = hitPoint - center;
        // 反射用のベクトル
        _boundDir = ((_dir + vector)).normalized;

        Debug.Log(_dir.normalized);
        Debug.Log(vector.normalized);
        //_centerPoint = _hitPoint + _boundDir * 10;
        float rad = Mathf.Atan2(vector.y, vector.x);

        if (_debug)
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
        _sin = Mathf.Sin(Time.time * _sinSpeed);
        _alpha -= 1 * Time.deltaTime * _secondTime;
        _lineRenderer.startColor = new Color(1, 1, 1, _alpha);
        _lineRenderer.endColor = new Color(1, 1, 1, _alpha);
        Vector3 point = _pos * _sin * _dent;
        _centerPoint = point;
        if(_alpha < 0)
        {
            _sin = 0;
            SetType(LINE_TYPE.NON);
            foreach (var obj in _falseObj)
            {
                obj.SetActive(false);
                _alpha = 1.0f;
            }
        }
    }

    public float CheckRay()
    {

        // ボールが入ってきたベクトル方向に壁がないか調べる
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
        // ボールの物理挙動を切る
        _ballRigidbody.bodyType = RigidbodyType2D.Kinematic;
        _ballRigidbody.velocity = Vector3.zero;
        // ボールをへこみの中心まで移動させる
        _ball.transform.position = Vector3.Lerp(_ball.transform.position, _vertexPoint - (_boundDir * _ballSize.x / 2), Time.deltaTime * _speed);
        // 膜を伸ばす
        _centerPoint = Vector3.Lerp(_centerPoint, _hitPoint + _boundDir * dent, Time.deltaTime * _speed);
        // 膜の中心から伸ばせる長さと現在の伸びを比べる
        if ((_hitPoint + _boundDir * dent).magnitude - _centerPoint.magnitude <= 0.1f)
        {
            // 伸びた位置の逆
            _centerReversePos = -_centerPoint;
            _pos = (_centerPoint.normalized);

            // 物理挙動を有効化する
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
        // 線を曲げる
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
        //SetCurve(_firstPoint, _endPoint);
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
