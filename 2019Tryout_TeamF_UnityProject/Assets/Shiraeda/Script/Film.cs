using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Film : MonoBehaviour
{
    // フェイドクラス
    private FadeSprite _fade;
    // 跳ね返せる強さ
    private float _boundPower;
    // 当たり判定
    [SerializeField, Tooltip("角度")]
    private Collider2D _collider;

    public float BoundPower
    {
        get { return _boundPower; }
        set { _boundPower = value; }
    }

    [SerializeField]
    private LineBezier _bezier = null;
    [SerializeField]
    private BallScript ball;

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
        //foreach(Transform child in transform)
        //{
        //    _fade = child.GetComponent<FadeSprite>();
        //}
    }

    private void Update()
    {
        _bezier.AngleMoveDown(transform.up);
    }

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("球をはじいた");
        // フェイドアウトスタート
        if (_bezier == null)
        {
            Debug.Log("足りないコンポーネントが存在します");
            return;
        }

        // 衝突したオブジェクトがボールか
        if (collision.transform.tag == "Ball")
        {
            _bezier.AngleMoveDown(transform.forward);
            Rigidbody2D rigid = collision.transform.gameObject.GetComponent<Rigidbody2D>();
            Debug.Log("コルーチンを開始します");
            foreach (ContactPoint2D point in collision.contacts)
            {
                _bezier.HitCheck(point.point, ball.Veloctiy, 1);
            }
            StopAllCoroutines();
            StartCoroutine(_bezier.Extend());
            _collider.enabled = false;
        }
    }
}
