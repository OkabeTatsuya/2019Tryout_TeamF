using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manaeger : SingletonMonoBehaviour<Manaeger>
{
    public GameManagerData m_managerData;
    float m_gameScoer;
    float m_hitCount;

    bool m_gameStart;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {

    }

    void GameStart()
    {
        if (m_gameStart)
        {

        }
    }


    void ScoerCreate()
    {

    }


    void CreateObejct()
    {


    }


    void AddScoer(int num)
    {
        m_gameScoer += num;
    }
}
