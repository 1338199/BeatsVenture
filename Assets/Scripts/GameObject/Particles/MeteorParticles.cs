using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//流星粒子效果
public class MeteorParticles : Particles
{
    //伤害
    public int damageAmount = 3;

    //粒子系统
    private ParticleSystem ps;

    public override void Start()
    {
        //粒子动画结束后消失
        ps = GetComponent<ParticleSystem>();
        Destroy(this.gameObject, ps.main.duration);

    }

    //粒子与怪物发生碰撞
    public override void OnParticleCollision(GameObject other)
    {
        if (other != null)
        {
            //扣除怪物血量
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
                enemyHealth.TakeDamage(damageAmount);
        }
    }
}
