using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    [SerializeField] GameManagerData m_gameData;
    [SerializeField] string m_keyName;

    public void SendUI()
    {
        //Manaeger.Instance.AddUIObject(this.gameObject, m_keyName);
    }


}
