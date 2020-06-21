using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttack : EnemyAttack
{
    private Animator anim;
    public DarkbcController darkbc;
    // Start is called before the first frame update
    protected override void Start()
    {
        anim = this.transform.parent.GetComponent<Animator>();
        enemyController = this.transform.parent.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    public override void Attack(GameObject gameObject)
    {
        anim.SetTrigger("Attack");
        
        gameObject.GetComponent<HealthController>().TakeDamage(damage);
        //    GameObject.Find("Directional Light").SetActive(false);
        darkbc.setLast();
    }
}
