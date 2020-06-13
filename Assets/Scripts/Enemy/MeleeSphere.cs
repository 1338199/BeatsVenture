using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSphere : MonoBehaviour
{

    public int damageAmount = 5;

    public float attackCoolDown = 2f;  //攻击的冷却时间

    private EnemyController enemyController;

    private float attackTimer = 0;
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        enemyController = this.transform.parent.GetComponent<EnemyController>();

        StartCoroutine(StartAttackCoolDown());
    }


    void Attack(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (attackTimer >= attackCoolDown)
            {
                ps.Play();
                other.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
                UIController.Instance.ShowWaring();
                StartCoroutine(StartAttackCoolDown());
                enemyController.isHitPlayer = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Attack(other);
    }

    private void OnTriggerStay(Collider other)
    {
        Attack(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            ps.Stop();
            enemyController.isHitPlayer = false;
        }
    }

    IEnumerator StartAttackCoolDown()
    {
        attackTimer = 0;

        while (attackTimer <= attackCoolDown)
        {
            attackTimer += Time.deltaTime;
            yield return null;
        }
    }


}
