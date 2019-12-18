using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slime_Gauge : MonoBehaviour
{
    [SerializeField]
    Image HPGauge;
    [SerializeField]
    private Transform targetObject;
    [SerializeField]
    private GameObject slime;
    private RectTransform uiImage;
    private Vector3 offset = new Vector3(0, 2f, 0);
    Slime script;

    void Start()
    {
        uiImage = GetComponent<RectTransform>();//UIの位置情報の取得
        script = slime.GetComponent<Slime>();//スライムの情報の取得
    }

    void Update()
    {
        uiImage.position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetObject.position + offset);//UIをスライムに追従
        var Min = script.hp;
        var Max = script.hp_max;
        HPGauge.fillAmount = (float)Min / Max;//ゲージの増減

    }
}
