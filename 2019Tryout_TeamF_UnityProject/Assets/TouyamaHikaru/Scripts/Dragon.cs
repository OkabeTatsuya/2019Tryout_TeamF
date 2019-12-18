using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    private bool directionFlag; //true:左右 false:上下

    // Start is called before the first frame update
    void Start()
    {
        // transformを取得
        Transform myTransform = this.transform;

        enemyX = myTransform.position.x;
        enemyY = myTransform.position.y;

        directionSwitch = true;
        direction = true;
        directioncount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Lateral_Vertical_Movement2();
    }

    

}
