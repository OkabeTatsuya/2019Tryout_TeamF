using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shot1 : Bullet
{

    private List<GameObject> list = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Create()
    {
        GameObject obj = ShotClone();
        if(obj != null)
        {
            list.Add(obj);
        }

    }

    private void Move()
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] != null)
            {
                list[i].transform.position -= new Vector3(0, E_ShotSpeed, 0);

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
