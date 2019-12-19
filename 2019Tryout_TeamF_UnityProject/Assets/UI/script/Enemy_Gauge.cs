using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Gauge : MonoBehaviour
{
    [SerializeField]
    Image HPGauge;
    [SerializeField]
    GameObject enemy;
    private RectTransform uiImage;
    private Vector3 offset = new Vector3(0, 1f, 0);//UIを頭上に表示させる
    Enemy script;
    Transform pos;

    void Start()
    {
        uiImage = GetComponent<RectTransform>();//UIの位置情報の取得
        script = enemy.GetComponent<Enemy>();//エネミーの情報の取得
        pos = enemy.GetComponent<Transform>();
    }

    void Update()
    {
        uiImage.position = RectTransformUtility.WorldToScreenPoint(Camera.main, pos.position + offset);//UIをエネミーに追従
        var Min = script.hp;
        var Max = script.hp_max;
        HPGauge.fillAmount = (float)Min / Max;//ゲージの増減

    }
}
