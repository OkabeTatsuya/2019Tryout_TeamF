using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dragon_Gauge : MonoBehaviour
{
    [SerializeField]
    Image HPGauge; // HP ゲージ
    [SerializeField]
    private GameObject dragon;
    Dragon script;//Dragonスクリプトの参照

    void Start()
    {
        script = dragon.GetComponent<Dragon>();//ドラゴンの情報の取得
    }

    void Update()
    {
        var Min = script.hp;
        var Max = script.hp_max;
        HPGauge.fillAmount = (float)Min / Max;

    }
}
