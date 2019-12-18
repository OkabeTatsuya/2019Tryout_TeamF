using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class EnemyPop : SingletonMonoBehaviour<EnemyPop>
{
    public SatgeDatabase Stage_Database;
    public GameObject[] EnemyPoint;
    int nowWave = 0, enemyCou = 0;

    void Start()
    {
        Enemy_Set();
    }
    void Update()
    {

    }



    void Enemy_Set()
    {
        Import_MapData Data;
        Data = JsonConvert.DeserializeObject<Import_MapData>(Stage_Database.Stage.Wave[nowWave].ToString());
        for (int i = 0; i < Data.MapData.Count; i++)
        {
            GameObject Enemy = Instantiate(Stage_Database.EnemyList[Data.MapData[i].EnemyNo]);
            Enemy.transform.parent = EnemyPoint[nowWave].transform;
            Enemy.transform.localPosition = new Vector3(Data.MapData[i].X, -Data.MapData[i].Y, 0);
            Enemy.transform.localScale = new Vector3(Data.MapData[i].size, Data.MapData[i].size, 1);
        }
        
    }

    public class Import_MapData
    {
        public List<EnemyData> MapData;
    }
}