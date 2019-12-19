using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(LineBezier))]
public class CurveCollision : MonoBehaviour
{
    // コンポーネント取得
    private PolygonCollider2D _polyCollider;
    private LineRenderer _lineRenderer;
    private LineBezier _linebezier;
    // 当たり判定の太さ
    [SerializeField]
    private float _Thickness = 1.0f;
    private List<Vector2> _pointList = new List<Vector2>();
    private List<Vector2> _setPointList = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        _polyCollider = GetComponent<PolygonCollider2D>();
        _lineRenderer = GetComponent<LineRenderer>();
        _linebezier = GetComponent<LineBezier>();
    }

    private void CollderSet()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
