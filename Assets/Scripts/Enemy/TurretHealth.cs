using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealth : EnemyHealth
{

    private Boolean die;
    private GameObject explosion;
    private GameObject explosionClone;

    private void Awake()
    {
        explosion = GameObject.Find("Explosion");
        explosion.SetActive(false);
    }

    public override void UpdateInfo()
    {
        healthSilder.value = hp;
        if (hp <= 0 & !die)
            Die();
    }

    public override void Die()
    {
        explosionClone = Instantiate(explosion) as GameObject;
        explosionClone.transform.position = gameObject.transform.position;
        explosionClone.transform.rotation = Quaternion.identity;
        explosionClone.SetActive(true);
        explosionClone.GetComponent<ParticleSystem>().Play();
        die = true;
        Money();
        GetComponent<TurretController>().enabled = false;
        Destroy(this.gameObject);
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

    public override void Money()
    {
        isDie = true;
        int money = UnityEngine.Random.Range(60, 80);
        Coins.money += money;
    }
}
