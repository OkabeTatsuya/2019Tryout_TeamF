using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreate : MonoBehaviour
{
    [SerializeField, Tooltip("マウスクラス")]
    private MousePoint _mouse = null;
    // まく
    [SerializeField]
    private Film _film;
    private LineRenderer _guideLine;
    //「まく」の設定角度
    private float _angle;
    // タッチ座標保存変数
    private Vector3[] _touchPos;
    [SerializeField]
    private LineBezier _bezier = null;
    // Start is called before the first frame update
    void Start()
    {
        _guideLine = GetComponent<LineRenderer>();
        _touchPos = new Vector3[2];
    }

    private void Create()
    {
        if (_mouse == null)
        {
            Debug.Log("コンポーネントが存在しない");
            return;
        }
        // Android移植用
        // 1か所以上タッチされているなら(Android版)
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _touchPos[0] = _mouse.GetWorldPoint();
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {

            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _touchPos[1] = _mouse.GetWorldPoint();
            }
        }
        else
        {
            // 押した瞬間
            if (Input.GetMouseButtonDown(0))
            {
                // 始点の座標を取得
                _touchPos[0] = _mouse.GetWorldPoint();
                _guideLine.SetPosition(0, _touchPos[0]);
                _bezier.StartCurve();
                _film.gameObject.SetActive(false);
            }
            if (Input.GetMouseButton(0))
            {
                // 終点座標を取得
                _touchPos[1] = _mouse.GetWorldPoint();
                _guideLine.SetPosition(1, _touchPos[1]);
                float distance = _mouse.GetDistance(_touchPos[0], _touchPos[1]);
                if (distance > 2)
                {
                    _bezier.transform.gameObject.SetActive(true);
                    _bezier.SetCurve(_touchPos[0], _touchPos[1]);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                _film.gameObject.SetActive(true);
                _touchPos[1] = _mouse.GetWorldPoint();
                // lineを生成
                float distance = _mouse.GetDistance(_touchPos[0], _touchPos[1]);
                if (distance > 2)
                {
                    _bezier.SetCurve(_touchPos[0], _touchPos[1]);
                }
                _bezier.Angle =  _mouse.GetAngle(_touchPos[0], _touchPos[1]);
                StartCoroutine(_bezier.Adjustment());
                Distance(_film.gameObject);
                // 作成したラインの角度
                Debug.Log(_mouse.GetAngle(_touchPos[0], _touchPos[1]) + "膜の角度");
            }
        }
    }

    private void Thickness()
    {

    }

    private void Distance(GameObject obj)
    {
        float distance = _mouse.GetDistance(_touchPos[0], _touchPos[1]);
        if(obj == null)
        {
            return;
        }
        // 長さ変換
        obj.transform.localScale = new Vector3(distance, 1 - (distance / 10), obj.transform.localScale.z);
        // 厚さの調節
        if(obj.transform.localScale.y > 1)
        {
            obj.transform.localScale = new Vector3(obj.transform.localScale.x, 1, obj.transform.localScale.z);
        }
        else if (obj.transform.localScale.y < 0.1f)
        {
            obj.transform.localScale = new Vector3(obj.transform.localScale.x, 0.1f, obj.transform.localScale.z);
        }
        // 
        _film.BoundPower = (10 - distance) / 2;
        if(_film.BoundPower < 0)
        {
            _film.BoundPower = 0;   
        }
        obj.transform.position = _touchPos[0];
        obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _mouse.GetAngle(_touchPos[0], _touchPos[1])));
    }

    // Update is called once per frame
    void Update()
    {
        Create();
    }
}
