using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthController
{
    private Animator anim;
    private ParticleSystem hitPs;

    private void Awake()
    {
        hitPs = transform.Find("HitParticles").GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
    }

    public override void UpdateInfo()
    {
        hitPs.Play();
        base.UpdateInfo();
        if (hp <= 0)
            Die();
    }

    public override void Die()
    {
        this.GetComponent<EnemyController>().enabled = false;
        anim.SetTrigger("die");
    }

    IEnumerator StartSinking()   //通过动画事件调用下沉
    {
        Vector3 sinkPos = new Vector3(this.transform.position.x, -2, this.transform.position.z);
        while (Vector3.Distance(this.transform.position, sinkPos) > 0.1f)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, sinkPos, Time.deltaTime);
            yield return null;
        }
        Destroy(this.gameObject, 0.5f);
    }
}
