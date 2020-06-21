using Assets.Scripts.Player;
using UnityEngine;
using System.Collections;

public class ActivateChest : MonoBehaviour {

	public Transform lid, lidOpen, lidClose;	// Lid, Lid open rotation, Lid close rotation
	public float openSpeed = 5F;				// Opening speed
	public bool canClose;                       // Can the chest be closed
	public static int skillNum;
	public static bool[] activeSkills;
	public GameObject TreasureMonster;

	[HideInInspector]
	public bool _open;                          // Is the chest opened
	public bool _get;
    private void Awake()
    {
		try
		{
			activeSkills = skillUtils.loadSkills();
			skillNum = SkillNumUtils.loadSkillNum();
		}
		catch
		{
			activeSkills = new bool[4] { false, false, false, false };
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
			
			if (mors == 2 && skillNum < 4 && !_get)
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
			else if(mors < 2 && !_get)
            {
				Coins.money = Coins.money + Random.Range(50,100);
				_get = true;
            }
			else if (mors == 3 && !_get)
			{
				Vector3 pos;
				pos = new Vector3(this.transform.position.x - 3, this.transform.position.y, this.transform.position.z);
				Instantiate(TreasureMonster, pos, TreasureMonster.transform.rotation);
				_get = true;
			}

		}
	}
	
	void OnMouseDown(){
		Debug.Log("mouse down!");
		if(canClose) _open = !_open; else _open = true;
	}
}
