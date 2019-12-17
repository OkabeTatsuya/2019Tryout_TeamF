using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class EnemyPop : MonoBehaviour
{
    public SatgeDatabase Stage_Database;
    public GameObject te;
    void Awake()
    {
        Export_MapData Data;
        Data = JsonConvert.DeserializeObject<Export_MapData>(Stage_Database.Stage.Wave_1.ToString());

        for (int i = 0; i < Data.MapData.Count; i++)
        {
            GameObject Enemy = Instantiate(Stage_Database.EnemyList[Data.MapData[i].EnemyNo]);
            Enemy.transform.parent = this.transform;
            Enemy.transform.localPosition = new Vector3(Data.MapData[i].X, -Data.MapData[i].Y, 0);
            Enemy.transform.localScale = new Vector3(Data.MapData[i].size, Data.MapData[i].size, 1);
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}