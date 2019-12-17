using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public byte type;
    // クローンするオブジェクト
    [SerializeField, Tooltip("クローンする弾のPrefab")]
    public GameObject Shotprefab;
    public float E_ShotSpeed;
    [SerializeField, Tooltip("弾発射間隔")]
    private float _secondInterval = 1.0f;
    protected GameObject clone;
    protected float EnemyShotTime;
    protected float endTimeBullet;
    // 弾のリスト
    protected List<GameObject> _bulletList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Screen.width + "横");
        Debug.Log(Screen.height + "縦");
    }

    public void TimeShot()
    {
        EnemyShotTime += Time.deltaTime;
        if (EnemyShotTime >= _secondInterval)
        {
            EnemyShotTime = 0;
        }
    }

    public GameObject ShotClone()
    {
        EnemyShotTime += Time.deltaTime;

        if (EnemyShotTime >= _secondInterval)
        {
            Transform myTransform = this.transform;

            clone = Instantiate(Shotprefab, myTransform.position, Quaternion.identity);
            _bulletList.Add(clone);
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
