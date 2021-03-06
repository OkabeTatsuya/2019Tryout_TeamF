﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "CreateScriptable/Manager/GameManagerData")]
public class GameManagerData : ScriptableObject
{

    public int[] m_maxTime;

    public Sprite[] m_ranckUIImage;

    public int[] m_rancScoer;

    public bool[] m_startVisivleUI;
    
    public int m_scoerDelta;
    
}
