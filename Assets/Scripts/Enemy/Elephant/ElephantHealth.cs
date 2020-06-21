using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantHealth : EnemyHealth
{
    public override void Money()
    {
        int money = UnityEngine.Random.Range(18, 22);
        Coins.money += money;
    }

    
}
