using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "CreateScriptable/Manager/GameManagerData")]
public class GameManagerData : ScriptableObject
{
    public enum UIName {
        Title,
        GameScene,
        Rezult,
        GameOver,
        Nown
    }

    public enum WaveNum
    {
        Wave1,
        Wave2,
        Wave3
    }

    //public GameObject[] m_uiObject;
    public UIName m_UIName;

    public int[] m_maxTime;

    public Sprite[] m_ranckUIImage;

    public int[] m_rancScoer;

    public bool[] m_startVisivleUI;
    
    public int m_scoerDelta;
}
