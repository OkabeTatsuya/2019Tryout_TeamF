using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Film : MonoBehaviour
{
    private Collider2D _collider;
    private FadeSprite _fade;
    private float _boundPower;
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
        if(_fade == null)
        {
            return;
        }
        //StartCoroutine(StartFade());
        _collider.enabled = false;
        gameObject.SetActive(false);
    }
}
