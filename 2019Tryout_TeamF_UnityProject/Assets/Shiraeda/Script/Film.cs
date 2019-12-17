using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Film : FadeSprite
{
    private Collider2D _collider;
    private float _boundPower;
    private float BoundPower
    {
        get { return _boundPower; }
        set { _boundPower = value; }
    }
    private void Awake()
    {
        Debug.Log("Awake");
        _collider = GetComponent<Collider2D>();
    }

    // オブジェクト有効化時の処理
    private void OnEnable()
    {
        Debug.Log("有効化された");
        _collider.enabled = true;
    }

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("球をはじいた");
        // フェイドアウトスタート
        StartCoroutine(StartFade());
        _collider.enabled = false;
        gameObject.SetActive(false);
    }
}
