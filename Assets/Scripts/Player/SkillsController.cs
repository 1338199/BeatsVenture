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

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();

        aoeSkills = GetComponentInChildren<AoeSkills>();
        flashSkills = GetComponentInChildren<FlashSkills>();
        frozenSkills = GetComponentInChildren<FrozenSkills>();
        playerUltimate = GetComponentInChildren<PlayerUltimateSkills>();
    }

    public void RealseSkills(int skillNumber)
    {
        switch (skillNumber)
        {
            case 1:
                StartCoroutine(aoeSkills.Release());
                break;
            case 2:
                StartCoroutine(flashSkills.Release());
                break;
            case 3:
                StartCoroutine(frozenSkills.Release());
                break;
            case 4:
                StartCoroutine(playerUltimate.Release());
                break;
            default:
                break;
        }
    }


}
