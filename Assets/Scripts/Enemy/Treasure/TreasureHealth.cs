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
        if (!isDie)
        {
            Money();
        }
    }


    public override void Money()
    {
        isDie = true;
        int money = UnityEngine.Random.Range(100, 150);
        Coins.money += money;
    }
}
