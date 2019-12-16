using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreate : MonoBehaviour
{
    private MousePoint _mouse;
    private Vector3[] _touchPos;
    [SerializeField, Tooltip("ラインオブジェクト")]
    private GameObject _sprite = null;
    // ゴムの角度
    private float _angle;
    // Start is called before the first frame update
    void Start()
    {
        _mouse = GetComponent<MousePoint>();
        _touchPos = new Vector3[2];
    }

    private void Create()
    {
        if (_mouse == null)
        {
            Debug.Log("コンポーネントが存在しない");
            return;
        }
        // 1か所以上タッチされているなら(Android版)
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _touchPos[0] = _mouse.GetMousePoint();
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _touchPos[1] = _mouse.GetMousePoint();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                _touchPos[0] = _mouse.GetMousePoint();
            }
            if (Input.GetMouseButtonUp(0))
            {
                _touchPos[1] = _mouse.GetMousePoint();
                PointAngle();
                Distance();
                Debug.Log("タッチ処理");
            }
        }
    }
    // タッチ位置の距離
    private void Distance()
    {
        // テストコード
        _sprite.transform.localScale = new Vector3(_mouse.GetDistance(_touchPos[0], _touchPos[1]) * 5,10 - (_mouse.GetDistance(_touchPos[0], _touchPos[1])), _sprite.transform.localScale.z);
        if(_sprite.transform.localScale.y > 5)
        {
            _sprite.transform.localScale = new Vector3(_sprite.transform.localScale.x, 5, _sprite.transform.localScale.z);
        }
        if (_sprite.transform.localScale.y < 1)
        {
            _sprite.transform.localScale = new Vector3(_sprite.transform.localScale.x, 1, _sprite.transform.localScale.z);
        }
        _sprite.transform.position = (_touchPos[0] + _touchPos[1]) / 2;
        _sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
    }

    // 線の厚さ
    private void Thickness()
    {

    }

    private void PointAngle()
    {
        _angle = _mouse.GetAngle(_touchPos[0], _touchPos[1]);
    }

    // Update is called once per frame
    void Update()
    {
        Create();
    }
}
