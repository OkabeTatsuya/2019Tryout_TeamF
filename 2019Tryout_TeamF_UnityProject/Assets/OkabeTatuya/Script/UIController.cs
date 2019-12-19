using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : UIBase
{
    [SerializeField] GameObject[] m_uiObject;

    [SerializeField] bool[] m_visibleUI;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetVisble();
        VisibleUI();
        InVisibleUI();
    }

    void GetVisble()
    {
        for (int i = 0; i < Manaeger.Instance.m_visibleUI.Length; i++)
        {
            m_visibleUI[i] = Manaeger.Instance.m_visibleUI[i];
        }
    }


    void VisibleUI()
    {
        for(int i = 0; i < m_visibleUI.Length; i++)
        {
            if (m_visibleUI[i])
            {
                m_uiObject[i].SetActive(true);           
            }
        }
    }

    void InVisibleUI()
    {
        for (int i = 0; i < m_visibleUI.Length; i++)
        {
            if (!m_visibleUI[i])
            {
                m_uiObject[i].SetActive(false);
            }
        }
    }

}
