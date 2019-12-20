using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public SatgeDatabase Stage_Database;
    public CameraMove Camera;
    public GameObject[] EnemyPoint;
    int nowWave = -1;
    int maxWave = 0;
    [HideInInspector] public int EnemyCou = 0;
    [SerializeField] GameObject Player;
    Rigidbody2D rig;
    int EnemyMax = 0;

    public void GatmeStart()
    {
        EnemyCou = 0;
        nowWave = -1;
        rig = Player.GetComponent<Rigidbody2D>();
        maxWave = Stage_Database.Stage.Wave.Length - 1;
        Wave_change();
        for (int i = 0; i < 3; i++)
        {
            Import_MapData Data;
            Data = JsonConvert.DeserializeObject<Import_MapData>(Stage_Database.Stage.Wave[nowWave].ToString());
            EnemyMax += Data.MapData.Count;
        }
    }

    public void PlayerOn()
    {
        Player.SetActive(true);
    }

    void Wave_change()
    {
        ++nowWave;
        Debug.Log(Stage_Database.Stage.Wave.Length);

        Import_MapData Data;
        Data = JsonConvert.DeserializeObject<Import_MapData>(Stage_Database.Stage.Wave[nowWave].ToString());
        EnemyCou = Data.MapData.Count;
        for (int i = 0; i < Data.MapData.Count; i++)
        {
            GameObject Enemy = Instantiate(Stage_Database.EnemyList[Data.MapData[i].EnemyNo]);
            Enemy.transform.parent = EnemyPoint[nowWave].transform;
            Enemy.transform.localPosition = new Vector3(Data.MapData[i].X, -Data.MapData[i].Y, 0);
            Enemy.transform.localScale = new Vector3(Data.MapData[i].size, Data.MapData[i].size, 1);
        }
        Camera.WaveStart();
    }

    public void Enemy_Del()
    {
        --EnemyCou;
        if (EnemyCou <= 0)
        {
            //ステージ最後
            if (nowWave >= maxWave)
            {
                rig.velocity = Vector3.zero;
                //リザルト
                Manaeger.Instance.GameEnd(true);
                Manaeger.Instance.ChangeUI(UIName.Rezult);
                
            }
            else
            {
                Wave_change();
            }
        }
    }

    public class Import_MapData
    {
        public List<EnemyData> MapData;
    }
}