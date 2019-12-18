using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public int hp = 5;
    public int hp_max = 5;

    void Update()
    {
        var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        var dv = 6f * Time.deltaTime * direction;
        transform.Translate(dv.x, dv.y, 0.0f);
        if (hp <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void click()
    {
        hp--;
    }
}
