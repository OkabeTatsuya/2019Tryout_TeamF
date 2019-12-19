using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{

    private bool firstPush = false;

    private bool DownPush = false;

    public GameObject[] GameSetObject;
    
    float speed = 0.01f;    //アルファ値を上げるスピード
    float red, green, blue, alpha;              //Imageのcolorの変数
    float red2, green2, blue2, alpha2;
    float red3, green3, blue3, alpha3;
    float red4, green4, blue4, alpha4;
    [SerializeField] Image panelout;            //フェードアウトしたいものをここに入れる
    [SerializeField] Image panelout2;
    [SerializeField] Image panelout3;
    [SerializeField] Text panelin;              //フェードインしたいものをここに入れる
    [SerializeField] Sprite Pushimage;          //クリックしたときに画像を入れ替えるための画像を準備する
    [SerializeField] Sprite Pushimage2;          //クリックしたときに画像を入れ替えるための画像を準備する


    // Start is called before thpanee first frame update
    void Start()
    {
        red = panelout.color.r;
        green = panelout.color.g;
        blue = panelout.color.b;
        alpha = panelout.color.a;

        red2 = panelout2.color.r;
        green2 = panelout2.color.g;
        blue2 = panelout2.color.b;
        alpha2 = panelout2.color.a;

        red3 = panelout3.color.r;
        green3 = panelout3.color.g;
        blue3 = panelout3.color.b;
        alpha3 = panelout3.color.a;

        red4 = panelin.color.r;
        green4 = panelin.color.g;
        blue4 = panelin.color.b;
        alpha4 = panelin.color.a;
        
    }

    // Update is called once per frame
    void Update()
    {


        if(firstPush == true)
        {
            fade();
        }

        if(alpha <= 0)
        {
            Manaeger.Instance.ChangeUI(UIName.GameScene);

            //paneloutのalphaの値が0になったら非表示にする
            panelout.gameObject.SetActive(false);
            panelout2.gameObject.SetActive(false);
            panelout3.gameObject.SetActive(false);

            Manaeger.Instance.GameStart();
        }
    }

    public void ClickUp()
    {
        

        if (firstPush == false)
        {
            this.gameObject.GetComponent<Image>().sprite = Pushimage2;
        }
    }

    public void PressStart()
    {
        //DownPush = true;
        Debug.Log("Press Start!");
        this.gameObject.GetComponent<Image>().sprite = Pushimage;
    }

    public void ReleseButton()
    {
        //クリックして一度しか読まないようにする
        if (!firstPush)
        {
            Debug.Log("Go Next Scene!");

            firstPush = true;

            this.gameObject.GetComponent<Image>().sprite = Pushimage;
        }
    }
    void fade()
    {
        alpha -= speed;
        alpha2 -= speed;
        alpha3 -= speed;
        alpha4 += speed;
        
        panelout.color = new Color(red, green, blue, alpha);
        panelout2.color = new Color(red2, green2, blue2, alpha2);
        panelout3.color = new Color(red3, green3, blue3, alpha3);
        panelin.color = new Color(red4, green4, blue4, alpha4);

    }

}
