using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class skillUtils
    {
        static public void saveSkills(bool[] skills)
        {
            var skillStrBuilder = new StringBuilder();
            for (int i = 0; i != skills.Length; ++i)
            {
                if (skills[i])
                {
                    skillStrBuilder.Append('1');
                }
                else
                {
                    skillStrBuilder.Append('0');
                }
            }
            PlayerPrefs.SetString("skills", skillStrBuilder.ToString());
        }

        static public bool[] loadSkills()
        {
            var skillStr = PlayerPrefs.GetString("skills");
            var result = new bool[skillStr.Length];
            for (int i = 0; i != skillStr.Length; ++i)
            {
                result[i] = skillStr[i] == '1' ? true : false;
            }
            return result;
        }
    }
}
