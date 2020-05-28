using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthController : MonoBehaviour
{
    public int hp = 100;

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
        healthText.text = healthSilder.value.ToString();
    }

    public virtual void Die()
    {

    }
}
