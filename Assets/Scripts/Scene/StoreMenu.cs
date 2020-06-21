using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreMenu : MonoBehaviour
{
    private bool isShown = false;
    public int pureTimer;
    public Text pureTimerText;

    public int[] itemCost = {40, 60, 100, 50, 200, 100, 100, 500};
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        pureTimer = 0;
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
        pureTimer = 30;
    }

    IEnumerator pureTimerCountDown()
    {
        while (pureTimer >=0)
        {
            pureTimerText.GetComponent<Text>().text = pureTimer.ToString();
            yield return new WaitForSeconds(1);
            pureTimer--;
        }
    }

    public void buyItem(int id)
    {
        if (Coins.money < itemCost[id])
        {
            UIController.Instance.ShowInfo("Not enough money");
        }
        else
        {
            Coins.money -= itemCost[id];
            UIController.Instance.ShowInfo("Bug Item Successfully");
            switch (id)
            {
                case 0:
                    Debug.Log("Buy 0");
                    getCureItem();
                    break;
                case 1:
                    Debug.Log("Buy 1");
                    break;
                case 2:
                    Debug.Log("Buy 2");
                    break;
                case 3:
                    Debug.Log("Buy 3");
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
