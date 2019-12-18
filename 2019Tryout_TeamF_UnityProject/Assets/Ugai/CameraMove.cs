using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    float nowtime = 0f;
    float totaltime = 3f;

    float speed = 25;
    public GameObject[] WavePoint;
    bool WaveMove = false;
    void Start()
    {
        
    }
    float y = 0;
    Vector3 startPos;
    void Update()
    {
        //nowtime += Time.deltaTime;
        //transform.position = Vector3.Lerp(this.transform.position, new Vector3(transform.position.x, y, -10), nowtime / totaltime);
        

        if (Input.GetKeyDown(KeyCode.Return))
        {
            WaveMove = true;
            startPos = transform.position;
        }
        if (WaveMove)
        {
            //transform.position = Vector3.Lerp(startPos, WavePoint[0], this.transform.position)
            return;
        }

        if (player != null)
        {
            this.transform.position = new Vector3(transform.position.x, player.position.y, -10);
            Clamp();
        }
    }

    private Vector3 camera_pos;
    void Clamp()
    {
        camera_pos = transform.position; //プレイヤーの位置を取得

        camera_pos.y = Mathf.Clamp(camera_pos.y, 0, 48.0f); //x位置が常に範囲内か監視
        transform.position = new Vector3(camera_pos.x, camera_pos.y, -10); //範囲内であれば常にその位置がそのまま入る
    }
}
