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

    void Start()
    {
        maxWave = Stage_Database.Stage.Wave.Length - 1;
        Wave_change();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Wave_change();
        }
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
                //リザルト
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