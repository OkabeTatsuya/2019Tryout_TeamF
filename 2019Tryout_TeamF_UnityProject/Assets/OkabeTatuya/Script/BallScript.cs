using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallScript : MonoBehaviour
{
    [SerializeField] float m_speed;
    [SerializeField] Vector3 m_startMoveVector;
    [SerializeField] Vector3 m_stratPosition;


    // Start is called before the first frame update
    void Start()
    {
        ResetPosition();
        StartMove();
        AudioManager.Instance.PlaySound();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ResetPosition()
    {
        transform.position = m_stratPosition;
    }

    void StartMove()
    {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        Vector3 force = m_startMoveVector * m_speed;
        rb.AddForce(force);
    }
}
