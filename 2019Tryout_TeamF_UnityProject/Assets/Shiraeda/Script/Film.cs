using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Film : MonoBehaviour
{
    [SerializeField]
    private LineBezier _bezier = null;
    [SerializeField]
    private BallScript _ball;
    [SerializeField]
    private Collider2D[] colliders = new Collider2D[3];
    // フェイドクラス
    private FadeSprite _fade;
    // 跳ね返せる強さ
    private float _boundPower;
    // 当たり判定
    private Collider2D _collider;

    public float BoundPower
    {
        get { return _boundPower; }
        set { _boundPower = value; }
    }

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
    }

    private void Update()
    {
        _bezier.AngleMoveDown(transform.up);
    }

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_bezier == null)
        {
            Debug.Log("足りないコンポーネントが存在します");
            return;
        }

        // 衝突したオブジェクトがボールか
        if (collision.transform.tag == "Ball")
        {
            // 衝突時の判定
            foreach (ContactPoint2D point in collision.contacts)
            {
                _bezier.HitCheck(point.point, _ball.Veloctiy, 1);
            }

            StopAllCoroutines();
            Debug.Log("コルーチンを開始します");
            StartCoroutine(_bezier.Extend());
            foreach(Collider2D col in colliders)
            {
                col.enabled = false;
            }
        }
    }
}
