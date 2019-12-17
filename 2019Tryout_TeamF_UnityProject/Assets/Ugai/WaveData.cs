using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "MyScriptable/Create MapData")]
public class WaveData : ScriptableObject
{
    public Object Wave_1, Wave_2, Wave_3;
}