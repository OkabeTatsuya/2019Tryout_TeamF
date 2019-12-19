using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : UIBase
{
    [SerializeField] Text m_resultUI;
    [SerializeField] Image m_rankUI;

    // Start is called before the first frame update
    void Start()
    {
        SendUI();
        m_resultUI.text = Manaeger.Instance.m_gameScoer.ToString();
        m_rankUI.sprite = Manaeger.Instance.SetRankImage();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
