using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//流星粒子效果
public class PoisonSmoke : Particles
{
    //伤害
    public int damageAmount = 1;

    //粒子系统
    private ParticleSystem ps;

    public override void Start()
    {
        ////粒子动画结束后消失
        //ps = GetComponent<ParticleSystem>();
        //Destroy(this.gameObject, ps.main.duration);

    }

    //粒子与人物发生碰撞
    public override void OnParticleCollision(GameObject other)
    {
        if(UIController.Instance.pureTimer > 0)
        {
            return;
        }
        if (other != null)
        {
            HealthController healthController = other.GetComponent<HealthController>();
            if (healthController != null)
                UIController.Instance.ShowInfo("You are suffering from gas attack!");
                healthController.TakeDamage(damageAmount);
        }
    }
}
