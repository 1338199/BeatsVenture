using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantAttack : EnemyAttack
{

    

    private Animator anim;

    protected override void Start()
    {
        beatCanAttack = 3;
        anim = this.transform.parent.GetComponent<Animator>();
        enemyController = this.transform.parent.GetComponent<EnemyController>();
    }


    public override void Attack(GameObject gameObject)
    {
        anim.SetTrigger("move");
        gameObject.GetComponent<HealthController>().TakeDamage(damage);
    }
}
