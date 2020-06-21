using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreMenu : MonoBehaviour
{
    private bool isShown = false;

    public int[] itemCost = {40, 60, 100, 50, 200, 100, 100, 500};
    // Start is called before the first frame update
    void Start()
    {
        Coins.money += 100;
        gameObject.SetActive(false);
        Debug.Log("money1:" + Coins.money);
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
                    break;
                case 5:
                    Debug.Log("Buy 5");
                    break;
                case 6:
                    Debug.Log("Buy 6");
                    break;
                case 7:
                    Debug.Log("Buy 7");
                    break;
                default:
                    break;
            }
        }
    }
    
}
