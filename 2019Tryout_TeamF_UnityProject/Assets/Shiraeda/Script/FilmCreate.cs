using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmCreate : MonoBehaviour
{
    [SerializeField, Tooltip("マウスクラス")]
    private MousePoint _mouse = null;
    // ガイド用のラインレンダラーを生成(テスト版)
    private LineRenderer _line;
    // 生成できる「まく」のリスト
    [SerializeField]
    private List<GameObject> _filmList = new List<GameObject>();
    // 処理する「まく」のオブジェクト
    private GameObject _film;
    //「まく」の設定角度
    private float _angle;
    // タッチ座標保存変数
    private Vector3[] _touchPos;

    // Start is called before the first frame update
    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _film = null;
        _touchPos = new Vector3[2];
        foreach(Transform child in transform)
        {
            _filmList.Add(child.gameObject);
        }
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
            if (Input.GetMouseButton(0))
            {
                _touchPos[1] = _mouse.GetMousePoint();
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
    private void Distance()
    {
        float distance = _mouse.GetDistance(_touchPos[0], _touchPos[1]);
        if (distance < 0.5)
        {
            return;
        }
        CheckActiveFilm();
        if(_film == null)
        {
            return;
        }
        // テストコード 長さ変換
        _film.transform.localScale = new Vector3(distance * 5, 10 - distance, _film.transform.localScale.z);
        // 厚さの調節
        if(_film.transform.localScale.y > 5)
        {
            _film.transform.localScale = new Vector3(_film.transform.localScale.x, 5, _film.transform.localScale.z);
        }
        else if (_film.transform.localScale.y < 1)
        {
            _film.transform.localScale = new Vector3(_film.transform.localScale.x, 1, _film.transform.localScale.z);
        }
        _film.transform.position = (_touchPos[0] + _touchPos[1]) / 2;
        _film.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
    }

    // 有効化できる「まく」をチェックする関数
    public bool CheckActiveFilm()
    {
        _film = null;
        foreach (var file in _filmList)
        {
            // シーン内でオブジェクトのActiveのチェックする
            if(file.activeInHierarchy == false)
            {
                _film = file;
                _film.SetActive(true);
                return true;
            }
        }
        return false;
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
