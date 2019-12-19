using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject Player;
    Transform playerTar;
    float nowtime = 0f;
    float totaltime = 3f;

    float speed = 8;
    public Vector2[] WavePoint;
    Vector3 targetPos;
    int nowWave = -1;
    bool WaveMove = false;
    float Max_y, Min_y;
    Rigidbody2D rd2;
    void Start()
    {
        playerTar = Player.transform;
        rd2 = GetComponent<Rigidbody2D>();
    }
    float y = 0;
    Vector3 startPos;
    void Update()
    {
        if (WaveMove)
        {
            Vector2 moveDirection = (new Vector3(0, WavePoint[nowWave][0], -10) - transform.position);

            moveDirection *= speed * Time.deltaTime;


            rd2.MovePosition((Vector2)transform.position + moveDirection);


            //Waveの初期位置に着いた
            if (Vector3.Distance(new Vector3(0, WavePoint[nowWave][0], -10), transform.position) <= 0.01f)
            {
                transform.position = new Vector3(0, WavePoint[nowWave][0], -10);
                playerTar.position = new Vector3(0, transform.position.y, 0);
                WaveMove = false;
            }

            return;
        }

        if (Player != null)
        {
            //シンプルな追従カメラ
            this.transform.position = new Vector3(transform.position.x, playerTar.position.y, -10);
            Clamp();
        }
    }


    public void WaveStart()
    {
        //nowWave = setWave;
        nowWave++;
        if (nowWave >= WavePoint.Length)
        {
            nowWave = 0;
        }
        WaveMove = true;
        startPos = transform.position;
        Min_y = WavePoint[nowWave][0];
        Max_y = WavePoint[nowWave][1];

    }


    private Vector3 camera_pos;
    void Clamp()
    {
        camera_pos = transform.position; //プレイヤーの位置を取得

        camera_pos.y = Mathf.Clamp(camera_pos.y,Min_y, Max_y); //x位置が常に範囲内か監視
        transform.position = new Vector3(camera_pos.x, camera_pos.y, -10); //範囲内であれば常にその位置がそのまま入る
    }
}