using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    public enum BGMClipName
    {
        title,
        stage,
        gameClear,
        gameOver
    }

    public enum SEClipName
    {
        bat,
        dragon,
        damage,
        Rubber,
        Move,
        Reflection,
        Slime,
        Start
    }

    [SerializeField] AudioSource m_bgmSource, m_seSource;
    [SerializeField] BGMClipName m_bgmClip;
    [SerializeField] float m_fadeSpeed;
    [SerializeField] AudioClipData m_audioClip;
    [SerializeField] SceneNameData m_sceneName;
    BGMClipName[] m_bgmClipes = {
        BGMClipName.title,
        BGMClipName.gameClear,
        BGMClipName.gameClear,
        BGMClipName.gameOver,
    };


    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        SelectBGM();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectBGM()
    {
        for (int i = 0; i < m_sceneName.sceneName.Count-1; i++ )
        {
            if(m_sceneName.sceneName[i] == SceneManager.GetActiveScene().name)
            {
                PlayBGM(i);
            }
        }
    }

    //SEを再生する
    public void PlaySE(SEClipName name)
    {
        m_seSource.clip = m_audioClip.seClip[(int)name];
        //Invoke("", 4);
        m_seSource.PlayOneShot(m_audioClip.seClip[(int)name]);
    }
       
    public void PlayBGM(BGMClipName name)
    {
        m_bgmSource.clip = m_audioClip.bgmClip[(int)name];
        m_bgmSource.Play();
    }

    public void PlayBGM(int name)
    {
        m_bgmSource.clip = m_audioClip.bgmClip[name];
        m_bgmSource.Play();
    }


    public bool FadeBGM()
    {
        float volume = m_bgmSource.volume - Time.deltaTime * m_fadeSpeed;
        if (m_bgmSource.volume <= 0)
        {
            m_bgmSource.Stop();
        }
        m_bgmSource.volume = volume;

        if (m_bgmSource.volume <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
