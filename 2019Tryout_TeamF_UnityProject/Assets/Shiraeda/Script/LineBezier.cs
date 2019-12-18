using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBezier : MonoBehaviour
{
    // LineRenderer取得用変数
    private LineRenderer _lineRenderer;
    [SerializeField, Tooltip("始点のオブジェクト")]
    private Transform _firstObj = null;
    [SerializeField, Tooltip("終点のオブジェクト")]
    private Transform _endObj = null;
    [SerializeField, Tooltip("中間地点")]
    private GameObject _centerObj = null;
    [SerializeField, Tooltip("分割するポイント")]
    private int _positionCount = 2;
    [SerializeField, Tooltip("まくの伸びる位置")]
    private Vector3 _centerPoint;
    [SerializeField, Header("Debug")]
    private bool _debug = false;
    [SerializeField]
    private Transform _pointObj = null;
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        if(_lineRenderer != null)
        {
            _lineRenderer.positionCount = _positionCount;
        }
    }

    private Vector3 Curve()
    {
        if(_positionCount < 2)
        {
            return Vector3.zero;
        }
        if (_lineRenderer != null)
        {
            _lineRenderer.positionCount = _positionCount;
        }

        Vector3 center = ((_firstObj.position + _endObj.position) / 2) + _centerPoint;
        _lineRenderer.SetPosition(0, _firstObj.position);
        for(int i = 0; i < _positionCount; i++)
        {
            float t = (float)i / (_positionCount - 1);
            Debug.Log(t);
            Vector3 pointBezier = BezierCurve(_firstObj.transform.position, _endObj.transform.position, center, t);
            _lineRenderer.SetPosition(i, pointBezier);
        }

        _lineRenderer.SetPosition(_positionCount - 1, _endObj.position);

        return Vector3.zero;
    }

    private Vector3 BezierCurve(Vector3 startPoint, Vector3 endPoint, Vector3 center, float t)
    {
        Vector3 _point1 = Vector3.Lerp(startPoint, center, t);
        Vector3 _point2 = Vector3.Lerp(center, endPoint, t);
        Vector3 _point3 = Vector3.Lerp(_point1, _point2, t);
        Debug.Log(_point3);
        if(_debug)
        {
            _pointObj.position = _point3;
        }
        _centerObj.transform.position = center;
        return _point3;
    }

    // Update is called once per frame
    void Update()
    {
        Curve();
    }
}
