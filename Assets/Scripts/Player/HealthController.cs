using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthController : MonoBehaviour
{
    public int hp = 100;
    // actor为0表示非玩家，为1表示玩家
    public int actor;
    public Slider healthSilder;
    public Text healthText;

    public void TakeDamage(int amount)
    {
        hp -= amount;
        UpdateInfo();
    }

    public virtual void UpdateInfo()
    {
        healthSilder.value = hp;
        //healthText.text = healthSilder.value.ToString();
        //Debug.Log("Health:" + hp.ToString());
    }

    public virtual void Die()
    {

    }

    public void Recover(int amount)
    {
        if (hp + amount > 100)
        {
            hp = 100;
        }
        else
        {
            hp += amount;
        }
        UpdateInfo();
    }

    public bool FullHp()
    {
        return hp == 100;
    }
}
