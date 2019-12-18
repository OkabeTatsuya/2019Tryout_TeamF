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
    SatgeDatabase Data;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        data = target as GameDatabase;
        if (GUILayout.Button("Edit", GUILayout.Height(25)))
        {
            SatgeDatabase model = ScriptableObject.CreateInstance<SatgeDatabase>();

            int stageNo = data.StagList.Count + 1;
            //書き出し先のフォルダが存在しないのなら作成
            if (!Directory.Exists("Assets/GameDatabase"))
            {
                Directory.CreateDirectory("Assets/GameDatabase");
            }
            if (!Directory.Exists("Assets/GameDatabase/"+ stageNo + "_Stage"))
            {
                Directory.CreateDirectory("Assets/GameDatabase/" + stageNo + "_Stage");
            }
            AssetDatabase.SaveAssets();
            string Path = "Assets/GameDatabase/" + stageNo + "_Stage/" + stageNo + "_Stage.asset";
            // 該当パスにオブジェクトアセットを生成
            AssetDatabase.CreateAsset(model, Path);

            //データの書き出しを更新する
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(Path, ImportAssetOptions.ForceUpdate);
            Data = AssetDatabase.LoadAssetAtPath<SatgeDatabase>(Path);
            Data.StageNo = stageNo;
            data.StagList.Add(Data);
            AssetDatabase.SaveAssets();
        }
    }
}
#endif