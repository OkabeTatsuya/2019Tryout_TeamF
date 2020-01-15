using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreate : MonoBehaviour
{
    [SerializeField, Tooltip("マウスクラス")]
    private MousePoint _mouse = null;
    private LineRenderer _debugLine;
    //「まく」の設定角度
    private float _angle;
    // タッチ座標保存変数
    private Vector3[] _touchPos;
    [SerializeField]
    private LineBezier _bezier = null;
    private LineRenderer _line;
    [SerializeField]
    private Film _film = null;
    [SerializeField]
    private GameObject _fristObj = null;
    [SerializeField]
    private GameObject _endObj = null;
    private float _alpha = 1.0f;
    private SpriteRenderer[] _sprites;
    // Start is called before the first frame update
    void Start()
    {
        _touchPos = new Vector3[2];
        _sprites = new SpriteRenderer[2];
        _debugLine = GetComponent<LineRenderer>();
        _sprites[0] = _fristObj.GetComponent<SpriteRenderer>();
        _sprites[1] = _endObj.GetComponent<SpriteRenderer>();
        _line = _bezier.GetComponent<LineRenderer>();
        _fristObj.SetActive(false);
        _endObj.SetActive(false);
        _film.gameObject.SetActive(false);
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
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
            }
        }
        else
        {
            // 押した瞬間
            if (Input.GetMouseButtonDown(0))
            {
                if (_bezier.GetLineType() == LineBezier.LINE_TYPE.NON)
                {
                    // 始点の座標を取得
                    _touchPos[0] = _mouse.GetWorldPoint();
                    _debugLine.SetPosition(0, _touchPos[0]);
                    _fristObj.transform.position = _touchPos[0];
                    _bezier.StartCurve();
                    _film.gameObject.SetActive(false);
                    _fristObj.SetActive(true);
                    _endObj.SetActive(true);
                    _alpha = 0.5f;
                    for (int i = 0; i < _sprites.Length; i++)
                    {
                        _sprites[i].color = new Color(1, 1, 1, _alpha);
                    }
                    _line.startColor = new Color(1, 1, 1, _alpha);
                    _line.endColor = new Color(1, 1, 1, _alpha);
                }
            }
            if (Input.GetMouseButton(0))
            {
                if (_bezier.GetLineType() == LineBezier.LINE_TYPE.NON)
                {
                    // 終点座標を取得
                    _touchPos[1] = _mouse.GetWorldPoint();
                    _endObj.transform.position = _touchPos[1];
                    _debugLine.SetPosition(1, _touchPos[1]);
                    float distance = _mouse.GetDistance(_touchPos[0], _touchPos[1]);
                    if (distance > 2)
                    {
                        _bezier.transform.gameObject.SetActive(true);
                        _fristObj.SetActive(true);
                        _endObj.SetActive(true);
                        _bezier.SetPoints(_touchPos);
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                // 最低必要距離
                float distance = _mouse.GetDistance(_touchPos[0], _touchPos[1]);
                if (distance > 2 && _bezier.GetLineType() == LineBezier.LINE_TYPE.NON)
                {
                    _film.gameObject.SetActive(true);
                    // ベジェ曲線で曲げる
                    _touchPos[1] = _mouse.GetWorldPoint();
                    _endObj.transform.position = _touchPos[1];
                    _bezier.SetPoints(_touchPos);
                    _bezier.SetType(LineBezier.LINE_TYPE.START);
                    Distance(_film.gameObject);
                    _alpha = 1.0f;
                    for (int i = 0; i < _sprites.Length; i++)
                    {
                        _sprites[i].color = new Color(1, 1, 1, _alpha);
                    }
                    _line.startColor = new Color(1, 1, 1, _alpha);
                    _line.endColor = new Color(1, 1, 1, _alpha);

                }
                else if (_bezier.GetLineType() == LineBezier.LINE_TYPE.NON)
                {
                    // 線が描けない長さならすべてを非有効化する
                    _film.gameObject.SetActive(false);
                    _fristObj.SetActive(false);
                    _endObj.SetActive(false);
                }
            }
        }
    }

    // 線の厚さ変更
    private float Thickness()
    {
        float distance = _mouse.GetDistance(_touchPos[0], _touchPos[1]);
        return 1 / (distance);
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
        _film.BoundPower = (10 - distance) / 2;
        if (_film.BoundPower < 0)
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
