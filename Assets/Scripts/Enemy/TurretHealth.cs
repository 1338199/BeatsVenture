using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealth : EnemyHealth
{


    private void Awake()
    {
    }

    public override void UpdateInfo()
    {
        healthSilder.value = hp;
        if (hp <= 0)
            Die();
    }

    public override void Die()
    {
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
