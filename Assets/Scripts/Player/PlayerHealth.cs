using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : HealthController
{
    private Animator anim;
    public DeathMenu deathMenu;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void UpdateInfo()
    {
        anim.SetTrigger("hit");
        healthText.text = hp.ToString();
        Debug.Log("hp:" + healthText.text.ToString());
        base.UpdateInfo();
        if (hp <= 0)
            Die();
    }

    public override void Die()
    {
        this.GetComponent<PlayerController>().enabled = false;
        anim.SetTrigger("die");
        Debug.Log("showDeathMenu!");
        deathMenu.ShowDeathMenu();
        MusicController.getInstance().Pause();
    }
}
