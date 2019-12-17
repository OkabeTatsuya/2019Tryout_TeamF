using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtl : MonoBehaviour
{
    [SerializeField, Tooltip("キャラクターの移動速度")]
    private float _speed;
    struct InputButton
    {
    }
    void Start()
    {
    }

    private void GetInput()
    {
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= Vector3.right * _speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * _speed * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ダメージ");
    }
}
