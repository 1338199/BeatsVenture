using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class SkillNumUtils
    {
        static public void saveSkillNum(int skillNum)
        {
            var skillNumBuilder = new StringBuilder();
            skillNumBuilder.Append(skillNum);
            PlayerPrefs.SetString("skillNum", skillNumBuilder.ToString());
        }

        static public int loadSkillNum()
        {
            var result = PlayerPrefs.GetString("skillNum");
            int skillNum = int.Parse(result);
            return skillNum;
        }
    }
}
