using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using System.IO;

[CreateAssetMenu(menuName = "MyScriptable/Create GameDatabase")]
public class GameDatabase : ScriptableObject
{
    public List<SatgeDatabase> StagList;

}
#if UNITY_EDITOR
[CustomEditor(typeof(GameDatabase), true)]
public sealed class GameDatabaseEditor : Editor
{
    GameDatabase data;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        data = target as GameDatabase;
        if (GUILayout.Button("Edit", GUILayout.Height(25)))
        {
            SatgeDatabase model = ScriptableObject.CreateInstance<SatgeDatabase>();

            //書き出し先のフォルダが存在しないのなら作成
            if (!Directory.Exists("Assets/GameDatabase"))
            {
                Directory.CreateDirectory("Assets/GameDatabase");
            }
            if (!Directory.Exists("Assets/GameDatabase/StageData"))
            {
                Directory.CreateDirectory("Assets/GameDatabase/StageData");
            }
            if (!Directory.Exists("Assets/GameDatabase/StageData"))
            {
                Directory.CreateDirectory("Assets/GameDatabase/StageData/WaveData");
            }
            AssetDatabase.SaveAssets();
            // 該当パスにオブジェクトアセットを生成
            AssetDatabase.CreateAsset(model, "Assets/GameDatabase/StageData/" + "Stage" + (data.StagList.Count + 1).ToString() + ".asset");
            AssetDatabase.SaveAssets();
        }
    }
}
#endif