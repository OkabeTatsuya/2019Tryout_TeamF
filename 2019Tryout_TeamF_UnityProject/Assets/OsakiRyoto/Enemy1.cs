using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float moveX;     //どこまで動くか
    public float moveY;
    public float speedX;    //スピード
    public float speedY;
    protected float enemyX;      //置いた座標
    protected float enemyY;
    protected bool direction;  //X軸　false:左  true:右    //Y軸　false:上　true:下
    protected bool directionSwitch; //方向を変えるための変数   false:上下    true:左右
    protected bool direction2;      //コウモリ用方向切り替え
    protected float directionCount;  //方向を変えるたびにカウントが増える （コウモリ用）
    protected float directionCount2; //方向を変えるたびにカウントが増える （ドラゴン用）
    

    public int hp;      //現在のHP
    public int hp_max;  //HPの最高値

    ParticleSystem particle;

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

        
        //右に移動
        if (direction == true)
        {
            // 座標移動
            myTransform.Translate(speedX, 0, 0);

            if (enemyX + moveX <= myTransform.position.x)
            {
                direction = false;
            }
        }

        //左に移動
        else if (direction == false)
        {
            myTransform.Translate(-speedX, 0, 0);

            if (enemyX - moveX >= myTransform.position.x)
            {
                direction = true;
            }
        }
    }

    public void VerticalMovement()  //縦移動
    {
        // transformを取得
        Transform myTransform = this.transform;

        //上に移動
        if (direction == true)
        {
            //座標移動
            myTransform.Translate(0, speedY, 0);

            if (enemyY + moveY <= myTransform.position.y)
            {
                direction = false;
            }
        }

        //下に移動
        else if (direction == false)
        {
            myTransform.Translate(0, -speedY, 0);

            if (enemyY - moveY >= myTransform.position.y)
            {
                direction = true;
            }
        }
    }

    public void RhombusMovement()   //ひし形の移動
    {
        // transformを取得
        Transform myTransform = this.transform;


        if (direction == true)
        {
            if (directionCount == 0)
            {
                //座標移動
                myTransform.Translate(speedX, -speedY, 0);
                if (enemyX + moveX <= myTransform.position.x)
                {
                    directionCount = 1;
                }


            }

            else if (directionCount == 1)
            {
                //座標移動
                myTransform.Translate(-speedX, -speedY, 0);
                if (enemyY - moveY * 2 >= myTransform.position.y)
                {
                    directionCount = 2;
                }
            }

            else if (directionCount == 2)
            {
                //座標移動
                myTransform.Translate(-speedX, speedY, 0);
                if (enemyX - moveX >= myTransform.position.x)
                {
                    directionCount = 3;
                }
            }

            else if (directionCount == 3)
            {
                //座標移動
                myTransform.Translate(speedX, speedY, 0);
                if (enemyY <= myTransform.position.y)
                {
                    directionCount = 0;
                }
            }
        }

        if (direction == false)
        {
            if (directionCount == 0)
            {
                //座標移動
                myTransform.Translate(-speedX, -speedY, 0);
                if (enemyX - moveX >= myTransform.position.x)
                {
                    directionCount = 1;
                }


            }

            else if (directionCount == 1)
            {
                //座標移動
                myTransform.Translate(speedX, -speedY, 0);
                if (enemyY - moveY * 2 >= myTransform.position.y)
                {
                    directionCount = 2;
                }
            }

            else if (directionCount == 2)
            {
                //座標移動
                myTransform.Translate(speedX, speedY, 0);
                if (enemyX + moveX <= myTransform.position.x)
                {
                    directionCount = 3;
                }
            }

            else if (directionCount == 3)
            {
                //座標移動
                myTransform.Translate(-speedX, speedY, 0);
                if (enemyY <= myTransform.position.y)
                {
                    directionCount = 0;
                }
            }
        }







        // transformを取得
        //Transform myTransform = this.transform;


        //if (direction == true)
        //{
        //    directionCount += Time.deltaTime;
        //}
        //else
        //{
        //    directionCount -= Time.deltaTime;
        //}

        //switch((int)directionCount / 1 % 4)
        //{
        //    case 0:     //右下
        //        myTransform.Translate(speedX, -speedY, 0);
        //        break;
        //    case 1:     //左下
        //        myTransform.Translate(-speedX, -speedY, 0);
        //        break;
        //    case 2:     //左上
        //        myTransform.Translate(-speedX, speedY, 0);
        //        break;
        //    case 3:     //右上
        //        myTransform.Translate(speedX, speedY, 0);
        //        break;
        //}

    }

    public void Lateral_Vertical_Movement2()    //上下に２回ずつ動く処理
    {
        // transformを取得
        Transform myTransform = this.transform;

        if (directionSwitch == true && direction == true && directionCount2 != 4)  //横移動
        {
            // 座標移動
            myTransform.Translate(speedX, 0, 0);

            if (enemyX + moveX <= myTransform.position.x)
            {
                direction = false;
                directionCount2++;
            }
        }

        else if (directionSwitch == true && direction == false && directionCount2 != 4)
        {
            myTransform.Translate(-speedX, 0, 0);

            if (enemyX - moveX >= myTransform.position.x)
            {
                direction = true;
                directionCount2++;
            }
        }



        if (directionSwitch == false && direction == true && directionCount2 != 4)  //縦移動
        {
            //座標移動
            myTransform.Translate(0, speedY, 0);

            if (enemyY + moveY <= myTransform.position.y)
            {
                direction = false;
                directionCount2++;
            }
        }

        else if (directionSwitch == false && direction == false && directionCount2 != 4)
        {
            myTransform.Translate(0, -speedY, 0);

            if (enemyY - moveY >= myTransform.position.y)
            {
                direction = true;
                directionCount2++;
            }
        }

        if (directionCount2 == 4)
        {
            if (directionSwitch == true)
            {
                // 座標移動
                myTransform.Translate(speedX, 0, 0);

                if (enemyX <= myTransform.position.x)
                {

                    directionSwitch = false;
                    directionCount2 = 0;
                }
            }
            else
            {
                //座標移動
                myTransform.Translate(0, speedY, 0);

                if (enemyY <= myTransform.position.y)
                {
                    directionSwitch = true;
                    directionCount2 = 0;
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
            GameManager.Instance.Enemy_Del();
        }
    }

    //ヒットポイント減少
    public void Damage()
    {
        hp--;
    }

    public void OnCollisionEnter2D(Collision2D Ball)
    {
        if (Ball.gameObject.tag == "Ball")
        {
            particle = this.GetComponent<ParticleSystem>();
            particle.Play();
            Damage();
            DestroyEnemy();
        }

        if (Ball.gameObject.tag == "Wall")
        {
            directionCount2++;
            //directionCount += 2;

            directionCount += 3;
            if(directionCount >= 4)
            {
                directionCount -= 4;
            }


            if (direction == true)
            {
                direction = false;
               
            }

            else
            {
                direction = true;
            }

        }
    }
}
