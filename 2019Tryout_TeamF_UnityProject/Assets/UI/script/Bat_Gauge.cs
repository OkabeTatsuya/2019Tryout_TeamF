using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bat_Gauge : MonoBehaviour
{
    [SerializeField]
    Image HPGauge; // HP ゲージ
    [SerializeField]
    private Transform targetObject;
    [SerializeField]
    private GameObject slime;
    private RectTransform uiImage;
    private Vector3 offset = new Vector3(0, 2f, 0);
    Bat script;

    void Start()
    {
        uiImage = GetComponent<RectTransform>();
        script = slime.GetComponent<Bat>();
    }

    void Update()
    {
        uiImage.position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetObject.position + offset);
        var Min = script.hp;
        var Max = script.hp_max;
        HPGauge.fillAmount = (float)Min / Max;

    }
}
