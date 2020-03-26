using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ベジェ曲線を描くためのクラス
public class Bezier : MonoBehaviour
{
    // ベジェ曲線のポイント座標
    protected Vector3[] _point;
    // 始点座標
    protected Vector3 _firstPoint = Vector3.zero;
    // 終点座標
    protected Vector3 _endPoint = Vector3.zero;
    protected Vector2 _vertexPoint;

    // LineRenderer取得用変数
    protected LineRenderer _lineRenderer;
    [SerializeField]
    protected GameObject[] _bezierPoint;
    [SerializeField, Tooltip("中心点,まくの伸びる位置")]
    protected Vector3 _centerPoint;
    [SerializeField, Tooltip("分割するポイント")]
    protected int _positionCount = 2;
    [SerializeField]
    private SmakeBound _smake;

    // Start is called before the first frame update
    void Start()
    {
        _firstPoint = Vector3.zero;
        _endPoint = Vector3.zero;
        _vertexPoint = Vector2.zero;

        _lineRenderer = GetComponent<LineRenderer>();
        // 座標の初期化
        _point = new Vector3[3];
        for (int i = 0; i < _point.Length; i++)
        {
            _point[i] = Vector3.zero;
        }
    }

    public void SetPoint(Vector3[] point)
    {
        for(int i = 0; i < _point.Length; i++)
        {
            _point[i] = point[i];
        }
    }

    public Vector3 SetCurve()
    {
        SetCurve(_bezierPoint[0].transform.position, _bezierPoint[_bezierPoint.Length - 1].transform.position);
        return Vector3.zero;
    }

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
            Vector3 pointBezier = BezierCurve(first, end, center, t);
            _lineRenderer.SetPosition(i, pointBezier);
        }
        _vertexPoint = _lineRenderer.GetPosition(_positionCount / 2);
        return Vector3.zero;
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

    // Update is called once per frame
    void Update()
    {
        SetCurve();
        if(_smake.Active == true)
        {
            _centerPoint = _smake.Bound();
        }
    }
}
