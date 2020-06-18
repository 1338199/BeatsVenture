using Assets.Scripts.Player;
using UnityEngine;
using System.Collections;

public class ActivateChest : MonoBehaviour {

	public Transform lid, lidOpen, lidClose;	// Lid, Lid open rotation, Lid close rotation
	public float openSpeed = 5F;				// Opening speed
	public bool canClose;                       // Can the chest be closed
	public static int skillNum;
	public static bool[] activeSkills;
	public static int money;

	[HideInInspector]
	public bool _open;                          // Is the chest opened
	public bool _get;
    private void Awake()
    {
		try
		{
			activeSkills = skillUtils.loadSkills();
			money = MoneyUtils.loadMoney();
			skillNum = SkillNumUtils.loadSkillNum();
		}
		catch
		{
			activeSkills = new bool[4] { false, false, false, false };
			money = 0;
			skillNum = 0;
		}
		_get = false;
	}


    void Update () {
		if(_open){
			ChestClicked(lidOpen.rotation);
		}
		else{
			ChestClicked(lidClose.rotation);
		}
	}
	
	// Rotate the lid to the requested rotation
	void ChestClicked(Quaternion toRot){
		if (lid.rotation != toRot){
			lid.rotation = Quaternion.Lerp(lid.rotation, toRot, Time.deltaTime * openSpeed);
			int mors = Random.Range(0, 4);
			if (mors == 3 && skillNum < 4 && !_get)
			{
				int index = Random.Range(0, 4);
				while (true)
				{
					if (activeSkills[index])
					{
						index = Random.Range(0, 4);
					}
					else
					{
						break;
					}
				}
				activeSkills[index] = true;
				skillNum++;
				_get = true;
				//getSkillsRom.ShowSkills();
			}
			else if(mors < 3)
            {
				money = money + Random.Range(50,100);
            }
		}
	}
	
	void OnMouseDown(){
		Debug.Log("mouse down!");
		if(canClose) _open = !_open; else _open = true;
	}
}
