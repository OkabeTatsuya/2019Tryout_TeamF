using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallScript : MonoBehaviour
{
    Rigidbody2D m_thisRigidbody;
    [SerializeField] Film m_film;
    [SerializeField] float m_speed;
    [SerializeField] Vector3 m_startMoveVector;
    [SerializeField] Vector3 m_stratPosition;
    [SerializeField] float m_moveLimit;
    [SerializeField] AudioClip m_audioClip;

    // Start is called before the first frame update
    void Start()
    {
        ResetPosition();
        StartMove();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        MoveLimit();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(m_film == null)
        {
            return;
        }

        if(collision.transform.tag == "Film")
        {
            Boost();
        }

        AudioManager.Instance.PlaySE(AudioManager.SEClipName.Rubber);

    }

    private void Boost()
    {
        // 膜の大きさに合わせて強く跳ねる
        // 1がデフォルト値
        Debug.Log(m_film.BoundPower);
        m_thisRigidbody.velocity = m_thisRigidbody.velocity * (1 + m_film.BoundPower);
        m_film.BoundPower = 0f;
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
