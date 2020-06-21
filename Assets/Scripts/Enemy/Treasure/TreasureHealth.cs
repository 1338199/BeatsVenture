using UnityEngine;
using System.Collections;

public class TreasureHealth : EnemyHealth
{

    public override void Die()
    {
        this.GetComponent<EnemyController>().enabled = false;
        this.GetComponent<EnemyController>().isDie = true;
        this.GetComponent<EnemyController>().disableNav();
        anim.SetTrigger("Die");
        Destroy(this.gameObject, 0.9f);
        Money();
    }


    public override void Money()
    {
        int money = UnityEngine.Random.Range(8, 22);
        Coins.money += money;
    }
}
