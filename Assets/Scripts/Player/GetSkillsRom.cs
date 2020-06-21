using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using UnityEngine;

public class GetSkillsRom : MonoBehaviour
{
    public int activeNumber = 0;
    private int skillNum;
    private bool[] activeSkills;
    // Start is called before the first frame update
    void Awake()
    {
        //skillNum = GameObject.Find("Chest01_Basic").GetComponent<ActivateChest>().skillNum;
        //activeSkills = GameObject.Find("Chest01_Basic").GetComponent<ActivateChest>().activeSkills;
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }

        try
        {
            activeSkills = skillUtils.loadSkills();
        }
        catch
        {
            activeSkills = new bool[4] { false, false, false, false };
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ActivateChest.skillNum == 0)
        {
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(true);
            if (ActivateChest.activeSkills[0] && !activeSkills[0])
            {
                activeSkills[0] = true;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                UIController.Instance.ShowInfo("Press 1 to activate this AOESkill");

            }
            if (ActivateChest.activeSkills[1] && !activeSkills[1])
            {
                activeSkills[1] = true;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                UIController.Instance.ShowInfo("Press 2 to activate this FlashSkill");
            }
            if (ActivateChest.activeSkills[2] && !activeSkills[2])
            {
                activeSkills[2] = true;
                gameObject.transform.GetChild(2).gameObject.SetActive(true);
                UIController.Instance.ShowInfo("Press 3 to activate this FrozenSkill");
            }
            if (ActivateChest.activeSkills[3] && !activeSkills[3])
            {
                activeSkills[3] = true;
                gameObject.transform.GetChild(3).gameObject.SetActive(true);
                UIController.Instance.ShowInfo("Press 4 to summon meteors");
            }
        }
    }

    public void ShowSkills()
    {
        //skillNum = ActivateChest.skillNum;
        //activeSkills = ActivateChest.activeSkills;

    }

    public void OnDestroy()
    {
        skillUtils.saveSkills(activeSkills);
    }
}
