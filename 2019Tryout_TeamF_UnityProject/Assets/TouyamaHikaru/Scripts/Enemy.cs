using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public float moveX;     //どこまで動くか
    public float moveY;
    public float speedX;    //スピード
    public float speedY;
    protected float enemyX;      //置いた座標
    protected float enemyY;
    protected bool direction;  //X軸　false:左  true:右    //Y軸　false:上　true:下
    protected bool directionSwitch; //方向を変えるための変数   false:上下    true:左右
    protected byte directioncount;  //方向を変えるたびにカウントが増える 

    public int hp = 5;
    public int hp_max = 5;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LateralMovement()   //横移動
    {
        // transformを取得
        Transform myTransform = this.transform;

        if (direction == true)
        {
            // 座標移動
            myTransform.Translate(speedX, 0, 0);

            if (enemyX + moveX <= myTransform.position.x)
            {
                direction = false;
            }
        }

        else if (direction == false)
        {
            myTransform.Translate(-speedX, 0, 0);

            if (enemyX - moveX >= myTransform.position.x)
            {
                direction = true;
            }
        }
    }

    public void VerticalMovement()
    {
        // transformを取得
        Transform myTransform = this.transform;

        if (direction == true)
        {
            //座標移動
            myTransform.Translate(0, speedY, 0);

            if (enemyY + moveY <= myTransform.position.y)
            {
                direction = false;
            }
        }

        else if (direction == false)
        {
            myTransform.Translate(0, -speedY, 0);

            if (enemyY - moveY >= myTransform.position.y)
            {
                direction = true;
            }
        }
    }

    public void RhombusMovement()
    {

        // transformを取得
        Transform myTransform = this.transform;

        if (direction == true)
        {
            //座標移動
            myTransform.Translate(speedX, -speedY, 0);

            if (enemyX + moveX <= myTransform.position.x)
            {
                speedX *= -1;
            }

            if(enemyY - moveY*2 >= myTransform.position.y)
            {
                speedY *= -1;
                direction = false;
            }

        }

        else if (direction == false)
        {
            myTransform.Translate(speedX, -speedY, 0);

            if (enemyX - moveX >= myTransform.position.x)
            {
                speedX *= -1;
            }
            if (enemyY <= myTransform.position.y)
            {
                speedY *= -1;
                direction = true;
            }
        }

    }

    public void Lateral_Vertical_Movement2()
    {
        // transformを取得
        Transform myTransform = this.transform;

        if (directionSwitch == true && direction == true && directioncount != 4)  //横移動
        {
            // 座標移動
            myTransform.Translate(speedX, 0, 0);

            if (enemyX + moveX <= myTransform.position.x)
            {
                direction = false;
                directioncount++;
            }
        }

        else if (directionSwitch == true && direction == false && directioncount != 4)
        {
            myTransform.Translate(-speedX, 0, 0);

            if (enemyX - moveX >= myTransform.position.x)
            {
                direction = true;
                directioncount++;
            }
        }

        

        if (directionSwitch == false && direction == true && directioncount != 4)  //縦移動
        {
            //座標移動
            myTransform.Translate(0, speedY, 0);

            if (enemyY + moveY <= myTransform.position.y)
            {
                direction = false;
                directioncount++;
            }
        }

        else if (directionSwitch == false && direction == false&& directioncount != 4)
        {
            myTransform.Translate(0, -speedY, 0);

            if (enemyY - moveY >= myTransform.position.y)
            {
                direction = true;
                directioncount++;
            }
        }

        if (directioncount == 4)
        {


            if(directionSwitch == true)
            {
                // 座標移動
                myTransform.Translate(speedX, 0, 0);

                if (enemyX <= myTransform.position.x)
                {
                    
                    directionSwitch = false;
                    directioncount = 0;
                }
            }
            else
            {
                //座標移動
                myTransform.Translate(0, speedY, 0);

                if (enemyY <= myTransform.position.y)
                {
                    directionSwitch = true;
                    directioncount = 0;
                }

            }
        }

    }

    //死亡確認と処理
    public void DestroyEnemy()
    {
        if (hp <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    //ヒットポイント減少
    public void Damage()
    {
        hp--;
    }


}
