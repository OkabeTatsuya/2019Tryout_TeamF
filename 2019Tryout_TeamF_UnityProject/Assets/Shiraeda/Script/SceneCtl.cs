using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtl : MonoBehaviour
{
    // インスタンス
    public static SceneCtl instance;
    // シーンマネージャー
    private SceneManager scene;
    private void Awake()
    {
        if(instance != null)
        {
            // thisだとオブジェクトが残るのでループすると大量のオブジェクトが発生する
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void GetScene()
    {
    }

    public void NextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
