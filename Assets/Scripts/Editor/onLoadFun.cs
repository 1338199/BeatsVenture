using UnityEngine;
using UnityEditor;
using Assets.Scripts.Player;
[InitializeOnLoad]
public class OnLoadFunc
{
    static OnLoadFunc()
    {
        skillUtils.saveSkills(new bool[4] { false, false, false, false });
        MoneyUtils.saveMoney(1000);
        SkillNumUtils.saveSkillNum(0);
        PlayerPrefs.SetFloat("playerHP", 100);
    }
}
