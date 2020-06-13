using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : HealthController
{
    private Animator anim;
    public Text healthText;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void UpdateInfo()
    {
        anim.SetTrigger("hit");
        healthText.text = hp.ToString();
        base.UpdateInfo();
        if (hp <= 0)
            Die();
    }

    public override void Die()
    {
        anim.SetTrigger("die");
    }
}
