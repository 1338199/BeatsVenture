using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsController : MonoBehaviour
{
    private AoeSkills aoeSkills;
    private FlashSkills flashSkills;
    private FrozenSkills frozenSkills;
    private PlayerUltimateSkills playerUltimate;

    private PlayerController playerController;
    private int skillNum;
    private bool[] activeSkills;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        aoeSkills = GetComponentInChildren<AoeSkills>();
        flashSkills = GetComponentInChildren<FlashSkills>();
        frozenSkills = GetComponentInChildren<FrozenSkills>();
        playerUltimate = GetComponentInChildren<PlayerUltimateSkills>();
        //skillNum = GameObject.Find("Chest01_Basic").GetComponent<ActivateChest>().skillNum;
        //activeSkills = GameObject.Find("Chest01_Basic").GetComponent<ActivateChest>().activeSkills;
        

    }

    public void RealseSkills(int skillNumber)
    {
        skillNum = ActivateChest.skillNum;
        activeSkills = ActivateChest.activeSkills;
        if (skillNum == 0)
        {
            return;
        }
        switch (skillNumber)
        {
            case 1:
                if (activeSkills[0])
                {
                    StartCoroutine(aoeSkills.Release());
                }
                break;
            case 2:
                if (activeSkills[1])
                {
                    StartCoroutine(flashSkills.Release());
                }
                break;
            case 3:
                if (activeSkills[2])
                {
                    StartCoroutine(frozenSkills.Release());
                }
                break;
            case 4:
                if (activeSkills[3])
                {
                    StartCoroutine(playerUltimate.Release());
                }
                break;
            default:
                break;
        }
    }


}
