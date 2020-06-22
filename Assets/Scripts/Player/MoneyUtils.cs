using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class MoneyUtils
    {
        static public void saveMoney(int money)
        {
            var moneyBuilder = new StringBuilder();
            moneyBuilder.Append(money);
            PlayerPrefs.SetString("money", moneyBuilder.ToString());
        }

        static public int loadMoney()
        {
            var result = PlayerPrefs.GetString("money");
            //Debug.Log("result" + result);
            int money = int.Parse(result);
            return money;
        }
    }
}
