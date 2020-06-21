using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureController : EnemyController
{

    

    // Start is called before the first frame update
    public override void Start()
    {
        isFindPlayer = true;
        player = GameObject.Find("Player");
        base.Start();
    }

    
    
}
