using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantAttack : EnemyAttack
{

    

    private Animator anim;
    protected new int beatCanAttack = 3;  //修改该值去修改攻击节拍

    protected override void Start()
    {
        anim = this.transform.parent.GetComponent<Animator>();
        enemyController = this.transform.parent.GetComponent<EnemyController>();
    }


    public override void Attack(GameObject gameObject)
    {
        anim.SetTrigger("move");
        gameObject.GetComponent<HealthController>().TakeDamage(damage);
    }
}
