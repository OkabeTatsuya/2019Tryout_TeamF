using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suraimu : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        // transformを取得
        Transform myTransform = this.transform;

        enemyX = myTransform.position.x;
        enemyY = myTransform.position.y;

        direction = true;

        
    }

    // Update is called once per frame
    void Update()
    {

        LateralMovement();

    }

    //private void OnCollisionEnter2D(Collision2D Ball)
    //{
    //    if (Ball.gameObject.tag == "Ball")
    //    {
    //        Damage();
    //        DestroyEnemy();
    //    }
    //}

}
