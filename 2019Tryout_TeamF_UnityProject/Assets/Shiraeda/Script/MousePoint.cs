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
    // 表示用オブジェクト
    [SerializeField, Tooltip("入れ替えるマウスカーソル")]
    private GameObject _cursorObj = null;
    // デバック情報
    [SerializeField, Header("Debug")]
    private bool _debug = false;

    [SerializeField, Tooltip("デバック用表示物")]
    private GameObject _debugObj = null;

    // Start is called before the first frame update
    void Start()
    {
        _worldPos = Vector3.zero;
        _distance = 0f;
        _angle = 0f;
        CursorChange();
    }
    
    // カーソル変更関数
    public void CursorChange()
    {
        // 変更するカーソルが無ければ処理をスキップ
        if(_cursorObj == null)
        {
            return;
        }
        // カーソルを非表示に
        Cursor.visible = false;
        _cursorObj.SetActive(true);
    }

    // カーソル移動関数
    public void MoveCursor()
    {
        if(_cursorObj == null)
        {
            return;
        }
        _cursorObj.transform.position = _worldPos;
    }

    public Vector3 GetWorldPoint()
    {
        return _worldPos;
    }

    private void MouseToWorldPoint()
    {
        // マウス座標を取得
        Vector3 mousePos = Input.mousePosition;
        // スクリーンのXbox
        _worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        _worldPos.z = 10.0f;
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
        MouseToWorldPoint();
        MoveCursor();
    }
}
