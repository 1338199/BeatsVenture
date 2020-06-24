using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantHealth : EnemyHealth
{
    
    public override void Money()
    {
        isDie = true;
        int money = UnityEngine.Random.Range(70, 100);
        Coins.money += money;
    }

    
}
