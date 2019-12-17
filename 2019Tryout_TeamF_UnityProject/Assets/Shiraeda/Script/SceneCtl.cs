using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtl : MonoBehaviour
{
    public enum SCENE_TYPE
    {
        TITEL,
        STAGE1,
        STAGE2,
        STAGE3,
        RESULT,
        MAX
    }
    // インスタンス
    public static SceneCtl instance;
    // シーンマネージャー
    private SceneManager scene;
    [SerializeField, Tooltip("シーンのタイプ")]
    private SCENE_TYPE _type;
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

    public Scene GetScene()
    {
         return SceneManager.GetActiveScene();
    }

    public Scene GetScene(string str)
    {
        return SceneManager.GetSceneByName(str);
    }

    public void NextScene(SCENE_TYPE type)
    {
        switch (type)
        {
            case SCENE_TYPE.TITEL:
                break;
            case SCENE_TYPE.STAGE1:
                break;
            case SCENE_TYPE.STAGE2:
                break;
            case SCENE_TYPE.STAGE3:
                break;
            case SCENE_TYPE.RESULT:
                break;
            case SCENE_TYPE.MAX:
            default:
                break;
        }

        public void NextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetSceneType(SCENE_TYPE type)
    {
        _type = type;
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
}