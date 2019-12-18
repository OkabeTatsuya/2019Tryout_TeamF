using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "CreateScriptable/AudioData")]
public class AudioClipData : ScriptableObject
{
    public AudioClip[] bgmClip;
    public AudioClip[] seClip;

}
