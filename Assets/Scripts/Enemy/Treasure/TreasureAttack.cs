using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureAttack : EnemyAttack
{

    protected Animator animator;
    // Start is called before the first frame update
    protected override void Start()
    {
        enemyController = gameObject.GetComponentInParent<EnemyController>();
        animator = gameObject.GetComponentInParent<Animator>();
        
    }

    public override void Attack(GameObject gameObject)
    {
        animator.SetTrigger("Bite Attack");

      
        //    GameObject.Find("Directional Light").SetActive(false);
    }

    protected override IEnumerator AttackEffect(Collider other)
    {


        yield return new WaitForSeconds(0.4f);
        gameObject.GetComponent<HealthController>().TakeDamage(damage);
    }
}
