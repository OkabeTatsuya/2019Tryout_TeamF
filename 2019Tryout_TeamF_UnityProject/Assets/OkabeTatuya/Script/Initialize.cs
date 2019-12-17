using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialize
{
    private const string InitializeSceneName = "GameManager";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void RuntimeInitializeApplication()
    {

        if (!SceneManager.GetSceneByName(InitializeSceneName).IsValid())
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(InitializeSceneName, LoadSceneMode.Additive);
        }
    }

}
