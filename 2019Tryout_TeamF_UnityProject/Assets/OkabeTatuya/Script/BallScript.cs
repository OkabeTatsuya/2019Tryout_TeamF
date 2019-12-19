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
    [SerializeField] AudioClip m_audioClip;
    private Vector2 m_velocity;
    public Vector2 Veloctiy
    {
        get { return m_velocity; }
        set { m_velocity = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        ResetPosition();
        StartMove();
    }

    // Update is called once per frame
    void Update()
    {
        m_velocity = m_thisRigidbody.velocity;
    }
    private void FixedUpdate()
    {
        MoveLimit();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Film")
        {
            //Boost();
        }
        if (collision.transform.tag == "Enemy")
        {
            HitStop.Instance.SlowDown();
        }
        AudioManager.Instance.PlaySE(AudioManager.SEClipName.Rubber);
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
            m_thisRigidbody.velocity = m_thisRigidbody.velocity.normalized * m_moveLimit;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    AudioManager.Instance.PlaySE(AudioManager.SEClipName.Rubber);
    //    
    //}
}
