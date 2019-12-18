using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmCreate : MonoBehaviour
{
    [SerializeField, Tooltip("始点のオブジェクト")]
    private GameObject _firstObj;
    [SerializeField, Tooltip("終点のオブジェクト")]
    private GameObject _endObj;
    [SerializeField, Tooltip("マウスクラス")]
    private MousePoint _mouse = null;
    // 生成できる「まく」のリスト
    [SerializeField, Tooltip("フィルムオブジェクトのリスト")]
    private List<GameObject> _filmList = new List<GameObject>();
    [SerializeField, Tooltip("ガイド用イメージ")]
    private GameObject _guide;
    // まく
    private Film _film;
    private LineRenderer _guideLine;
    // 処理する「まく」のオブジェクト
    private GameObject _object;
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
        GameObject obj = new GameObject();
        // Prefabに設定されたオブジェクトを生成
        foreach (var film in _filmList)
        {
            _film = film.GetComponent<Film>();
        }
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
                _touchPos[0] = _mouse.GetMousePoint();
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {

            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _touchPos[1] = _mouse.GetMousePoint();
            }
        }
        else
        {
            // 押した瞬間
            if (Input.GetMouseButtonDown(0))
            {
                // 始点の座標を取得
                _touchPos[0] = _mouse.GetMousePoint();
                _bezier.transform.gameObject.SetActive(true);
            }
            if (Input.GetMouseButton(0))
            {
                // 終点座標を取得
                _touchPos[1] = _mouse.GetMousePoint();
                // lineを生成
                _bezier.SetCurve(_touchPos[0], _touchPos[1]);
                _guide.SetActive(true);
                Distance(_guide);
            }
            if (Input.GetMouseButtonUp(0))
            {
                _guide.SetActive(false);
                _touchPos[1] = _mouse.GetMousePoint();
                // lineを生成
                _bezier.SetCurve(_touchPos[0], _touchPos[1]);
                CheckActiveFilm();
                Distance(_film.gameObject);
            }
        }
    }
    private void Distance(GameObject obj)
    {
        float distance = _mouse.GetDistance(_touchPos[0], _touchPos[1]);
        if(obj == null)
        {
            return;
        }
        // テストコード 長さ変換
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
        if(_film.BoundPower < 0)
        {
            _film.BoundPower = 0;
        }
        obj.transform.position = _touchPos[0];
        obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _mouse.GetAngle(_touchPos[0], _touchPos[1])));
    }

    // 有効化できる「まく」をチェックする関数
    public bool CheckActiveFilm()
    {
        foreach (var file in _filmList)
        {
            // シーン内でオブジェクトのActiveのチェックする
            if(file.activeInHierarchy == false)
            {
                _object = file;
                _film.gameObject.SetActive(true);
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        Create();
    }
}
