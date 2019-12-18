using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shot3 : Bullet
{
    //プレイヤーオブジェクト
    public GameObject player;
    //敵の弾のリスト
    private List<GameObject> list = new List<GameObject>();

    private List<Vector3> vec = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Create()
    {
        GameObject obj = ShotClone();
        if (obj != null)
        {
            //敵の座標を変数posに保存
            var enemyPos = this.gameObject.transform.position;
            var randomPos = new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-2.0f, 5.0f),0);
            //プレイヤーの位置から敵の位置（弾の位置）を引く
            vec.Add(randomPos - enemyPos);

            list.Add(obj);

        }


    }

    private void Move()
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list != null)
            {
                list[i].transform.position += new Vector3(vec[i].x * E_ShotSpeed, vec[i].y * E_ShotSpeed, 0);
                GameObject obj = list[i];
                //list.Remove(obj);
                //BulletDestroy(list[i]);
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
