using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreMenu : MonoBehaviour
{
    private bool isShown = false;

    public int[] itemCost = {40, 60, 100, 50, 200, 100, 100, 500};

    public GameObject player;

    public GameObject bullet;

    public Text attackText;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        Debug.Log("money1:" + Coins.money);
        
        attackText.text = "30";

    }
    
    void Update()
    {
        
    }

    public void showStoreMenu()
    {
        isShown = true;
        Debug.Log("show shop2");
        gameObject.SetActive(true);
    }

    public void closeStoreMenu()
    {
        isShown = false;
        gameObject.SetActive(false);
    }

    // 没写完等会再写
    public void getCureItem()
    {
        UIController.Instance.pureTimer = 30;
    }

    public void buyItem(int id)
    {
        Debug.Log("money2:" + Coins.money);
        if (Coins.money < itemCost[id])
        {
            UIController.Instance.ShowInfo("Not enough money");
        }
        else
        {
            Coins.money -= itemCost[id];
            UIController.Instance.ShowInfo("Bug Item Successfully");
            HealthController healthController = player.GetComponent<HealthController>();
            Projectile projectile = bullet.GetComponent<Projectile>();
            switch (id)
            {
                case 0:
                    Debug.Log("Buy 0");
                    getCureItem();
                    break;
                case 1:
                    Debug.Log("Buy 1");
                    if (healthController != null)
                    {
                        healthController.Recover(20);
                    }
                    break;
                case 2:
                    Debug.Log("Buy 2");
                    if (healthController != null)
                    {
                        healthController.Recover(40);
                    }
                    break;
                case 3:
                    Debug.Log("Buy 3");
                    projectile.AddDamage();
                    attackText.text = projectile.damageAmout.ToString();
                    break;
                case 4:
                    Debug.Log("Buy 4");
                    if (!ActivateChest.activeSkills[0])
                    {
                        ActivateChest.activeSkills[0] = true;
                        ActivateChest.skillNum++;
                    }
                    else
                    {
                        Coins.money += itemCost[id];
                    }
                    break;
                case 5:
                    Debug.Log("Buy 5");
                    if (!ActivateChest.activeSkills[1])
                    {
                        ActivateChest.activeSkills[1] = true;
                        ActivateChest.skillNum++;
                    }
                    else
                    {
                        Coins.money += itemCost[id];
                    }
                    break;
                case 6:
                    Debug.Log("Buy 6");
                    if (!ActivateChest.activeSkills[2])
                    {
                        ActivateChest.activeSkills[2] = true;
                        ActivateChest.skillNum++;
                    }
                    else
                    {
                        Coins.money += itemCost[id];
                    }
                    break;
                case 7:
                    Debug.Log("Buy 7");
                    if (!ActivateChest.activeSkills[3])
                    {
                        ActivateChest.activeSkills[3] = true;
                        ActivateChest.skillNum++;
                    }
                    else
                    {
                        Coins.money += itemCost[id];
                    }
                    break;
                default:
                    break;
            }
        }
    }
    
}
