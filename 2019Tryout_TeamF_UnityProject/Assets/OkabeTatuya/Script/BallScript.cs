using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallScript : MonoBehaviour
{
    Rigidbody2D m_thisRigidbody;
    [SerializeField] float m_speed;
    [SerializeField] Vector3 m_startMoveVector;
    [SerializeField] Vector3 m_stratPosition;
    [SerializeField] float m_moveLimit;

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
        MoveLimit();
    }

    void ResetPosition()
    {
        transform.position = m_stratPosition;
    }

    void StartMove()
    {
        m_thisRigidbody = this.GetComponent<Rigidbody2D>();
        Vector3 force = m_startMoveVector * m_speed;

        m_thisRigidbody.AddForce(force);
    }

    void MoveLimit()
    {
        if (m_thisRigidbody.velocity.magnitude > m_moveLimit)
        {
            Debug.Log(m_thisRigidbody.velocity.magnitude.ToString());

            m_thisRigidbody.velocity = m_thisRigidbody.velocity.normalized * m_moveLimit;
        }
    }
}
