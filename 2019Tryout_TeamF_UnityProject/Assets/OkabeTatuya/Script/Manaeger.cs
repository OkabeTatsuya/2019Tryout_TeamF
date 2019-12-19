using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum  UIName
{
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

public class Manaeger : SingletonMonoBehaviour<Manaeger>
{
    public GameManagerData m_managerData;
    public int m_gameScoer;
    public int m_nowTime;
    public bool[] m_visibleUI= {true,false,false,false};
    public bool m_resetData;
    public bool m_nextWaveFlag;
    public bool m_addOnesFlag;
    public bool m_isRezualt;

    List<GameObject> m_uiObject;
    List<string> m_uiKey;

    float m_hitCount;

    public bool m_gameStart;

    int m_waveCount = 0;

    // Start is called before the first frame update
    void Start()
    {

        //m_visibleUI = m_managerData.m_startVisivleUI;
    }

    // Update is called once per frame
    void Update()
    {
        Hunder();
        ActhveUI();
        GotoRezult();

    }

    //選択されたUIを表示
    public void ChangeUI(UIName name)
    {

        Debug.Log((int)name);

        for (int i = 0; i < m_visibleUI.Length; i++)
        {
            if (i == (int)name)
            {
                m_visibleUI[i] = true;
            }
            else
            {
                m_visibleUI[i] = false;
            }
        }
    }


    public void AddWaveCount()
    {
        m_waveCount++;
    }

    public int ResetTimeCount()
    {
        if (m_waveCount < m_managerData.m_maxTime.Length - 1)
        {
            return m_managerData.m_maxTime[m_waveCount];
        }

        return 0;
    }

    void ActhveUI()
    {
        //m_managerData.m_uiObject[2].SetActive(true);
    }

    void GameStart()
    {
        if (m_gameStart)
        {

        }
    }

    void NextWave()
    {
        if (m_nextWaveFlag)
        {
            //次のウェーブに移動
            
        }
    }

    void CreateObejct()
    {

    }

    void Hunder()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            
            GameReStart();
        }
    }

    void GameReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("GameManager",LoadSceneMode.Additive);
    }
    
    public Sprite SetRankImage()
    {
        for (int i = 0; i <  m_managerData.m_rancScoer.Length; i++)
        {
            if (m_managerData.m_rancScoer[i] <= m_gameScoer)
            {
                return m_managerData.m_ranckUIImage[i];
            }
        }

        return null;
    }

    void VisibleUI(string key)
    {
        for(int i = 0; i < m_uiKey.Count; i++)
        {
            if (key == m_uiKey[i])
            {
                m_uiObject[i].SetActive(true);
            }
        }
    }

    public void AddScore()
    {
        m_gameScoer += m_nowTime * m_managerData.m_scoerDelta;
    }

    public void AddUIObject(GameObject obj, string key)
    {
        m_uiObject.Add(obj);
        m_uiKey.Add(key);
    }

    void GotoRezult()
    {
        if(m_isRezualt && Input.GetMouseButtonDown(0))
        {
            GameReStart();
        }
    }
}
