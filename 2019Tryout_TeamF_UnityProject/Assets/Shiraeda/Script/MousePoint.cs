using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePoint : MonoBehaviour
{
    // ワールド座標
    private Vector3 _worldPos;
    // 2点間の距離
    private float _distance;
    // 2点間の角度
    private float _angle;

    // デバック情報
    [SerializeField, Header("Debug")]
    private bool _debug = false;
    // 表示用オブジェクト
    [SerializeField, Tooltip("デバック用表示物")]
    private GameObject _debugObj = null;
    private GameObject _obj;

    // Start is called before the first frame update
    void Start()
    {
        _worldPos = Vector3.zero;
        _distance = 0f;
        _angle = 0f;

        if (_debug && _debugObj != null)
        {
            _obj = Instantiate(_debugObj);
        }
    }

    public Vector3 GetMousePoint()
    {
        // マウス座標を取得
        Vector3 mousePos = Input.mousePosition;
        // スクリーンのXbox
        _worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        _worldPos.z = 10.0f;
        if(_debug && _obj != null)
        {
            _obj.transform.position = _worldPos;
        }
        return _worldPos;
    }
    // 2点間の距離
    public float GetDistance(Vector2 pos1, Vector2 pos2)
    {
        _distance = Vector2.Distance(pos1, pos2);
        // 仮の戻り値
        return _distance;
    }
    // 2点間の角度の差
    public float GetAngle(Vector2 pos1, Vector2 pos2)
    {
        float vecX = (pos2.x - pos1.x);
        float vecY = (pos2.y - pos1.y);
        float rad = Mathf.Atan2(vecY, vecX);
        _angle = rad * Mathf.Rad2Deg;
        return _angle;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
