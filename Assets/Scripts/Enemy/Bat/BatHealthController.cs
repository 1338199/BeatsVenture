using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatHealthController : EnemyHealth
{
    // Start is called before the first frame update
    public override void Die()
    {
        this.GetComponent<EnemyController>().enabled = false;
        this.GetComponent<EnemyController>().isDie = true;
        this.GetComponent<EnemyController>().disableNav();
        anim.SetTrigger("Die");
        Destroy(this.gameObject, 0.9f);
    }
}
