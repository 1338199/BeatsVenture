using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttack : EnemyAttack
{
    protected Animator anim;
    public DarkbcController darkbc;
    // Start is called before the first frame update
    protected override void Start()
    {
        anim = this.transform.parent.GetComponent<Animator>();
        enemyController = this.transform.parent.GetComponent<EnemyController>();
    }

  
    public override void Attack(GameObject gameObject)
    {
        anim.SetTrigger("Attack");

        StartCoroutine(AttackEffect(gameObject));
    }

    protected override IEnumerator AttackEffect(GameObject other)
    {


        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<HealthController>().TakeDamage(damage);
        //    GameObject.Find("Directional Light").SetActive(false);
        darkbc.setLast();
    }
}
