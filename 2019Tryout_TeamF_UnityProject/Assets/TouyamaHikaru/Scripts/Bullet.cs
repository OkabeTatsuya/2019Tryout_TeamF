using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public byte type;
    public GameObject Shotprefab;
    public float E_ShotSpeed;
    protected GameObject clone;
    protected float EnemyShotTime;
    protected float endTimeBullet;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public GameObject ShotClone()
    {


        EnemyShotTime += Time.deltaTime;
        
        if (EnemyShotTime >= 1.0f)
        {
            Transform myTransform = this.transform;

            clone = Instantiate(Shotprefab, myTransform.position, Quaternion.identity);


            EnemyShotTime = 0;

            return clone;
        }

        return null;

    }



    public void BulletDestroy(GameObject Bullet)
    {
        endTimeBullet += Time.deltaTime;

        if (!(Bullet.transform.position.x > 0 && Bullet.transform.position.x < Screen.width)
            ||!(Bullet.transform.position.y > 0 && Bullet.transform.position.y < Screen.height))
        {
            Destroy(Bullet);
     
        }
    }


}
