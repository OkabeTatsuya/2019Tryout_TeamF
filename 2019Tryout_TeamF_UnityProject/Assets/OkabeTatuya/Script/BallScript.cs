using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallScript : MonoBehaviour
{
    Rigidbody2D m_thisRigidbody;
    [SerializeField] float m_speed;
    [SerializeField] Vector3 m_startMoveVector;
    [SerializeField] Vector3 m_stratPosition;
    [SerializeField] Vector2 m_filmJampVcetor;
    [SerializeField] float m_jumpPower;
    [SerializeField] float m_moveLimit;
    public CameraMove CameraObj;
    private Vector2 m_velocity;
    public Vector2 Veloctiy
    {
        get { return m_velocity; }
        set { m_velocity = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_thisRigidbody = this.GetComponent<Rigidbody2D>();

        ResetPosition();
        //StartMove();
    }

    // Update is called once per frame
    void Update()
    {
        m_velocity = m_thisRigidbody.velocity;

        if (!GetComponent<SpriteRenderer>().isVisible)
        {
            CameraObj.Ball_Return();
        }
    }

    private void FixedUpdate()
    {
        MoveLimit();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Film")
        {
        }
        if (collision.transform.tag == "Enemy")
        {
            HitStop.Instance.SlowDown();
        }
        if (collision.transform.tag == "StageFilm")
        {
            Debug.Log("sdg");
            HitStageFilm();
        }
        AudioManager.Instance.PlaySE(AudioManager.SEClipName.Rubber);
    }

    void ResetPosition()
    {
        transform.position = m_stratPosition;
    }

    void MoveLimit()
    {
        if (m_thisRigidbody.velocity.magnitude > m_moveLimit)
        {
            m_thisRigidbody.velocity = m_thisRigidbody.velocity.normalized * m_moveLimit;
        }
    }

    void HitStageFilm()
    {
        m_thisRigidbody.velocity = m_thisRigidbody.velocity * (1 + m_jumpPower);

    }
}
