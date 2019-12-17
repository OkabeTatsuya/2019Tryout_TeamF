using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shot2 : Bullet
{
    private List<GameObject> list = new List<GameObject>();
    private bool direction_random;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Create()
    {
        GameObject obj = ShotClone();
        if (obj != null)
        {
            list.Add(obj);

            if(Random.Range(0,2) == 1)
            {
                direction_random = true;
            }
            else
            {
                direction_random = false;
            }
        }


    }

    private void Move()
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] != null)
            {
                if (direction_random == true)
                {
                    list[i].transform.position -= new Vector3(E_ShotSpeed, E_ShotSpeed, 0);
                }
                else
                {
                    list[i].transform.position -= new Vector3(-E_ShotSpeed, E_ShotSpeed, 0);
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        Create();
        Move();
    }
}
