using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReStart : MonoBehaviour
{
    public Rigidbody2D Player;
    void Start()
    {
        Cursor.visible = true;
        Player.velocity = Vector3.zero;
        Player.isKinematic = true;
        Player.GetComponent<Collider2D>().enabled = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Manaeger.Instance.GameReStart();
        }
    }
}
