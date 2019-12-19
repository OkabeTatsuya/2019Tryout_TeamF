using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLine : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetPoint()
    {

    }

    public void SetPointLine(int index, Vector2 position)
    {
        if(_lineRenderer == null)
        {
            Debug.Log("コンポーネントがアタッチされてません");
            return;
        }
        _lineRenderer.SetPosition(index, position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
