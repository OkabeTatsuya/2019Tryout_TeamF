using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : SingletonMonoBehaviour<HitStop>
{
    //　Time.timeScaleに設定する値
    [SerializeField]
    private float timeScale = 0.1f;
    //　時間を遅くしている時間
    [SerializeField]
    private float slowTime = 0.5f;
    //　経過時間
    private float elapsedTime = 0f;
    //　時間を遅くしているかどうか
    public bool isSlowDown = false;

    void Update()
    {
        //　スローダウンフラグがtrueの時は時間計測
        if (isSlowDown)
        {
            elapsedTime += Time.unscaledDeltaTime;
            if (elapsedTime >= slowTime)
            {
                SetNormalTime();
            }
        }
    }
    //　時間を遅らせる処理
    public void SlowDown()
    {
        if (!isSlowDown)
        {
            elapsedTime = 0f;
            Time.timeScale = timeScale;
            isSlowDown = true;
        }
    }
    //　時間を元に戻す処理
    public void SetNormalTime()
    {
        isSlowDown = false;
        Time.timeScale = 1f;
    }

    //public void SlowOn(Collider Coll)
    //{
    //    if (Coll.gameObject.tag == "Enemy")
    //    {
    //        SlowDown();
    //    }
    //}
}
