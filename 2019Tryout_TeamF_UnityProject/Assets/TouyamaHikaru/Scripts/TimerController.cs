using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Text timerText;  //タイマーテキスト

    public float totalTime;

    int seconds;

    // Use this for initialization
    void Start()
    {
        totalTime = Manaeger.Instance.m_managerData.m_maxTime[0];

    }

    // Update is called once per frame
    void Update()
    {

        //タイマーテキストに指定したタイマーを経過時間で減算し代入
        totalTime -= Time.deltaTime;
        seconds = (int)totalTime;
        timerText.text = seconds.ToString();
    }
}