using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using System.IO;
using Newtonsoft.Json;

[CreateAssetMenu(menuName = "MyScriptable/Create Database")]
public class SatgeDatabase : ScriptableObject
{
    public int StageNo;
    public int MapScale_X = 16, MapScale_Y = 9;

    public WaveData Stage;
    public GameObject[,] MapData;
    public List<GameObject> EnemyList;
}




#if UNITY_EDITOR
[CustomEditor(typeof(SatgeDatabase), true)]
public sealed class DatabaseEditor : Editor
{
    SatgeDatabase data;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        data = target as SatgeDatabase;
        //try
        //{
        //    if (data.Stage != null)
        //    {
        //        if (GUILayout.Button("Edit", GUILayout.Height(25)))
        //        {

        //        }
        //    }
        //    else
        //    {
        //        if (GUILayout.Button("Create", GUILayout.Height(25)))
        //        {
        //            MapEditor window = EditorWindow.GetWindow<MapEditor>("MapEditor");
        //            window.Set(data);
        //        }
        //    }

        //}
        ////データの破損や関係ないデータが入っていた場合
        //catch
        //{
        //    Debug.Log("Missing_Correction");
        //    circuitObject.Puzzle_CompleteData = null;
        //    if (GUILayout.Button("Create", GUILayout.Height(25)))
        //    {
        //        window = EditorWindow.GetWindow<PuzzleEditor>("PuzzleEditor");
        //        window.Setup(true, circuitObject, null);
        //        Debug.Log("Create");
        //    }
        //}


        if (GUILayout.Button("Edit", GUILayout.Height(25)))
        {
            MapEditor window = EditorWindow.GetWindow<MapEditor>("MapEditor");
            window.Set(data);
        }

    }
}
//----------------------------------------------------------------
//マップ設定エディタ
//----------------------------------------------------------------
public class MapEditor : EditorWindow
{
    [SerializeField] private Set_Data[] editData;
    SatgeDatabase dataBase;
    int Data_Scale_X = 0, Data_Scale_Y = 0;
    public void Set(SatgeDatabase data)
    {
        dataBase = data;
        Scale_X = dataBase.MapScale_X;
        Scale_Y = dataBase.MapScale_Y;

        editData = new Set_Data[Scale_X];
        for (int x = 0; x < Scale_X; x++)
        {
            editData[x] = new Set_Data();
            editData[x].Map_Data = new MapData[Scale_Y];

            for (int y = 0; y < Scale_Y; y++)
            {
                if (data != null)
                {
                    if (x >= Data_Scale_X || y >= Data_Scale_Y)
                    {
                        MapData Data = new MapData();
                        editData[x].Map_Data[y] = Data;
                    }
                    else
                    {
                        //editData[x].Map_Data[y] = circuit[x, y];
                    }
                }
                else
                {
                    MapData Data = new MapData();
                    editData[x].Map_Data[y] = Data;
                }
            }
        }
        enemyTexture = new Texture2D[dataBase.EnemyList.Count];
        none = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Sprite/" + "none" + ".png");
        for (int i = 0; i < enemyTexture.Length; i++)
        {
            enemyTexture[i] = dataBase.EnemyList[i].GetComponent<SpriteRenderer>().sprite.texture;
        }
    }

    private Texture2D none;
    private Texture2D[] enemyTexture;
    private int Scale_X = 0, Scale_Y = 0;
    void OnEnable()
    {
        
    }

    public struct Enemy_area
    {
        public Vector3 Center;
        public float Radius;
        public int X, Y;
    }

    private int panelScale = 50;
    private float enemySize = 1;
    private Vector2 _scrollPosition = Vector2.zero, _crollPosition_tool = Vector2.zero;
    private int setEnemy = 0;
    private List<Enemy_area> Circle;
    
    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Export", GUILayout.Width(75), GUILayout.Height(30)))
        {
            Export();
        }

        panelScale = EditorGUILayout.IntSlider("ズーム", panelScale, 25, 50);

        enemySize = EditorGUILayout.FloatField("エネミーサイズ", enemySize);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        _crollPosition_tool = EditorGUILayout.BeginScrollView(_crollPosition_tool, GUI.skin.box, GUILayout.Width(100));
        GUILayout.BeginVertical();

        //出現するエネミーのリストを表示
        for (int i = 0; i < dataBase.EnemyList.Count; i++)
        {
            if (setEnemy == i)
            {
                GUI.color = new Color(155.0f / 255, 1, 1, 1);
            }
            if (GUILayout.Button(enemyTexture[i], GUILayout.Width(75), GUILayout.Height(75)))
            {
                setEnemy = i;
            }
            GUI.color = Color.white;
        }


        GUILayout.EndVertical();

        EditorGUILayout.EndScrollView();
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUI.skin.box);

        //スクロールの領域確保処理
        GUI.color = Color.clear;
        GUILayout.Box("", GUILayout.Width(panelScale * 2 + panelScale * Scale_X), GUILayout.Height(panelScale * 2 + panelScale * Scale_Y));
        GUI.color = Color.white;

        //敵の判定エリア情報
        Circle = new List<Enemy_area>();
        //パネル表示
        for (int x = 0; x < Scale_X; x++)
        {
            for (int y = 0; y < Scale_Y; y++)
            {
                Handles.color = Color.red;

                Rect rect = new Rect(panelScale + panelScale * x, panelScale + panelScale * y, panelScale, panelScale);


                if (GUI.Button(rect, none, GUIStyle.none))
                {
                    //右クリック
                    if (Event.current.button == 0)
                    {
                        if (enemySize > 0)
                        {
                            editData[x].Map_Data[y].EnemyPop = true;
                            editData[x].Map_Data[y].size = enemySize;
                            editData[x].Map_Data[y].EnemyNo = setEnemy;
                        }
                    }
                    //左クリック
                    else if (Event.current.button == 1)
                    {
                        editData[x].Map_Data[y] = new MapData();
                    }
                }

                if (editData[x].Map_Data[y].EnemyPop)
                {
                    Enemy_area circleData = new Enemy_area();
                    circleData.Center = new Vector3(panelScale + panelScale * x + (panelScale / 2), panelScale + panelScale * y + (panelScale / 2), 0);
                    circleData.Radius = (25 * (panelScale / 50.0f)) * editData[x].Map_Data[y].size;
                    circleData.X = x;
                    circleData.Y = y;
                    Circle.Add(circleData);
                }
            }
        }

        //敵キャラクターの描画
        for(int i = 0;i < Circle.Count;i++)
        {
            float size = panelScale * editData[Circle[i].X].Map_Data[Circle[i].Y].size;
            float rect_X = panelScale + panelScale * Circle[i].X;
            float rect_Y = panelScale + panelScale * Circle[i].Y;
            rect_X += (panelScale / 2) * (1 - editData[Circle[i].X].Map_Data[Circle[i].Y].size);
            rect_Y += (panelScale / 2) * (1 - editData[Circle[i].X].Map_Data[Circle[i].Y].size);

            Rect texRect = new Rect(rect_X, rect_Y, size, size);
            Handles.DrawWireDisc(Circle[i].Center, new Vector3(0, 0, 1), Circle[i].Radius);
            GUI.DrawTexture(texRect, enemyTexture[editData[Circle[i].X].Map_Data[Circle[i].Y].EnemyNo]);
        }


        //スクロール箇所終了
        EditorGUILayout.EndScrollView();

        GUILayout.EndHorizontal();
    }
    void Update()
    {
        Repaint();
    }


    private string json;
    UnityEngine.Object exportData;
    void Export()
    {

        int loop = 0;
        float progress = 0;
        Export_MapData Data = new Export_MapData();

        Data.MapData = new List<EnemyData>();
        for (int x = 0; x < editData.Length; x++)
        {
            for (int y = 0; y < editData[x].Map_Data.Length; y++)
            {
                if (editData[x].Map_Data[y].EnemyPop)
                {
                    EnemyData Set = new EnemyData();
                    Set.EnemyNo = editData[x].Map_Data[y].EnemyNo;
                    Set.size = editData[x].Map_Data[y].size;
                    Set.X = x;
                    Set.Y = y;
                    Data.MapData.Add(Set);
                }

                //書き出し状況をバーで表示
                progress = (float)loop / (editData.Length * editData[x].Map_Data.Length);
                EditorUtility.DisplayProgressBar("Exporting...", (progress * 100).ToString("F2") + "%", progress);
                loop++;
            }
        }
        Debug.Log(Data.MapData.Count);
        //書き出し先のフォルダが存在しないのなら作成

        if (!Directory.Exists("Assets/GameDatabase"))
        {
            Directory.CreateDirectory("Assets/GameDatabase");
        }
        if (!Directory.Exists("Assets/GameDatabase/" + dataBase.StageNo+ "_Stage"))
        {
            Directory.CreateDirectory("Assets/GameDatabase/" + dataBase.StageNo + "_Stage");
        }
        if (!Directory.Exists("Assets/GameDatabase/" + dataBase.StageNo + "_Stage/WaveData"))
        {
            Directory.CreateDirectory("Assets/GameDatabase/" + dataBase.StageNo + "_Stage/WaveData");
        }
        string Path = "Assets/GameDatabase/" + dataBase.StageNo + "_Stage/WaveData/Stage" + dataBase.StageNo + "_Wave.json";
        json = JsonConvert.SerializeObject(Data, Formatting.Indented);

        File.WriteAllText(Path, json);

        //データの書き出しを更新する
        AssetDatabase.SaveAssets();
        AssetDatabase.ImportAsset(Path, ImportAssetOptions.ForceUpdate);
        exportData = AssetDatabase.LoadMainAssetAtPath(Path);

        if (!dataBase.Stage)
        {
            WaveData model = ScriptableObject.CreateInstance<WaveData>();
            AssetDatabase.SaveAssets();
            string path = "Assets/GameDatabase/" + dataBase.StageNo + "_Stage/WaveData//WaveData.asset";
            model.Wave_1 = exportData;
            AssetDatabase.CreateAsset(model, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            WaveData data = AssetDatabase.LoadAssetAtPath<WaveData>(path);
            dataBase.Stage = data;
        }
        else
        {
            dataBase.Stage.Wave_1 = exportData;
        }

        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(dataBase.Stage);
        AssetDatabase.SaveAssets();
        EditorUtility.ClearProgressBar();
    }
}

//編集中データクラス
[System.Serializable]
public class Set_Data
{
    [SerializeField] public MapData[] Map_Data;
}
[System.Serializable]
public class MapData
{
    public float size = 1;
    public int EnemyNo = -1;
    public bool EnemyPop = false;
}

//書き出し用データクラス
public class Export_MapData
{
    public List<EnemyData> MapData;
}
#endif
