using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmakeBound : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _time;
    private float _nowTime;
    private float _size = 0.5f;
    private bool _active = false;
    public bool Active
    {
        get { return _active; }
    }
    // Start is called before the first frame update
    void Start()
    {
        _nowTime = 0;
        _active = false;
    }

    public Vector3 Bound()
    {
        _nowTime += Time.deltaTime * _speed;
        return new Vector3(0, Mathf.Sin(Time.time * _speed) * _size, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(_nowTime > _time)
        {
            _active = false;
            _nowTime = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.tag);
        if(collision.transform.tag == "Ball")
        {
            _active = true;
            _nowTime = 0;
        }
    }
}
