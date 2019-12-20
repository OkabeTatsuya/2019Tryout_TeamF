using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ベジェ曲線を描くためのクラス
public class Bezier : MonoBehaviour
{
    // 曲線用のLineRenderer
    protected LineRenderer _lineRenderer;
    [SerializeField]
    protected GameObject[] _bezierPoint;
    [SerializeField, Tooltip("中心点")]
    protected Vector3 _centerPoint;
    [SerializeField, Tooltip("反発速度")]
    protected float _speed = 1.0f;
    [SerializeField]
    protected int _positionCount = 2;
    [SerializeField]
    private SmakeBound _smake;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public Vector3 SetCurve()
    {
        Vector3 firstPoint = _bezierPoint[0].transform.position;
        Vector3 endPoint = _bezierPoint[_bezierPoint.Length - 1].transform.position;

        if (_positionCount < 2)
        {
            return Vector3.zero;
        }
        if (_lineRenderer != null)
        {
            _lineRenderer.positionCount = _positionCount;
        }
        // 現在の始点と終点の間から動いた距離
        Vector3 center = ((firstPoint + endPoint) / 2) + _centerPoint;
        // 始点の座標をセット
        _lineRenderer.SetPosition(0, _bezierPoint[0].transform.position);
        // 終点の座標をセット
        _lineRenderer.SetPosition(_positionCount - 1, _bezierPoint[_bezierPoint.Length - 1].transform.position);
        // 中間地点の座標をセット
        for (int i = 0; i < _positionCount; i++)
        {
            float t = (float)i / (_positionCount - 1);
            Vector3 pointBezier = BezierCurve(firstPoint, endPoint, center, t);
            _lineRenderer.SetPosition(i, pointBezier);
        }
        return Vector3.zero;
    }

    public Vector3 BezierCurve(Vector3 startPoint, Vector3 endPoint, Vector3 center, float t)
    {
        // ポイント1、ポイント2、ポイント3からなる2次元のベジェ曲線
        Vector3 _point1 = Vector3.Lerp(startPoint, center, t);
        Vector3 _point2 = Vector3.Lerp(center, endPoint, t);
        Vector3 _point3 = Vector3.Lerp(_point1, _point2, t);
        return _point3;
    }

    public void BezierCurve()
    {

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
