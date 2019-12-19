using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Film : MonoBehaviour
{
    // 当たり判定
    private Collider2D _collider;
    // フェイドクラス
    private FadeSprite _fade;
    // 跳ね返せる強さ
    private float _boundPower;
    public float BoundPower
    {
        get { return _boundPower; }
        set { _boundPower = value; }
    }

    [SerializeField]
    private LineBezier _bezier = null;

    private void Awake()
    {
        Debug.Log("Awake");
        _collider = GetComponent<Collider2D>();
        _boundPower = 0;
    }

    // オブジェクト有効化時の処理
    private void OnEnable()
    {
        Debug.Log("有効化された");
        _collider.enabled = true;
    }

    private void Start()
    {
        foreach(Transform child in transform)
        {
            _fade = child.GetComponent<FadeSprite>();
        }
    }

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("球をはじいた");
        // フェイドアウトスタート
        if (_fade == null || _bezier == null)
        {
            Debug.Log("足りないコンポーネントが存在します");
            return;
        }
        // 衝突したオブジェクトがボールか

        if (collision.transform.tag == "Ball")
        {
            _bezier.Hit();

            foreach (ContactPoint2D point in collision.contacts)
            {
                _bezier.HitPoint(point.point);
            }
            _collider.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
