using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enetes : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ball")
        {
            Debug.Log("bbbbb");
            GameManager.Instance.Enemy_Del();
        }
    }

}
