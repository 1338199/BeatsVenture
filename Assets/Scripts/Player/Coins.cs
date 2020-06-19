using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Player;

public class Coins : MonoBehaviour
{
    public static int money;
    public Text coinText;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            money = MoneyUtils.loadMoney();
            Debug.Log("money:"+MoneyUtils.loadMoney());
            coinText.text = money.ToString();
        }
        catch
        {
            money = 0;
            coinText.text = money.ToString();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = money.ToString();
    }
    public void OnDestroy()
    {
        MoneyUtils.saveMoney(money);
    }
}
