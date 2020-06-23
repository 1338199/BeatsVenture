using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureController : EnemyController
{

    protected new int beatCanMove = 1;    //修改该值去修改移动节拍

    // Start is called before the first frame update
    public override void Start()
    {
        isFindPlayer = true;
        player = GameObject.Find("Player");
        base.Start();
    }

    
    
}
