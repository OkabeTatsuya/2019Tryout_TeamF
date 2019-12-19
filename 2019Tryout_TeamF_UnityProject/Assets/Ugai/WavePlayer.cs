using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePlayer : MonoBehaviour
{

    public GameObject block;
    Vector3 offset;
    Vector3 target;
    float deg;

    IEnumerator ThrowBall()
    {
        float b = Mathf.Tan(deg * Mathf.Deg2Rad);
        float a = (target.y - b * target.x) / (target.x * target.x);

        for (float x = 0; x <= this.target.x; x += 0.3f)
        {
            float y = a * x * x + b * x;
            transform.position = new Vector3(x, y, 0) + offset;
            yield return null;
        }
    }

    public void SetTarget(Vector3 target, float deg)
    {
        this.offset = transform.position;
        this.target = target - this.offset;
        this.deg = deg;

        StartCoroutine("ThrowBall");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            float a = GetAim(this.transform.position, new Vector3(0, this.transform.position.y + 10, this.transform.position.z));
            SetTarget(block.transform.position, 0);
        }
    }

    public float GetAim(Vector2 p1, Vector2 p2)
    {
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        float rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }
}
