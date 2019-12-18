using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_Suraimu : Enemy
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
        VerticalMovement();
    }
}
