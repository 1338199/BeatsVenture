using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeSkills : PlayerSkills
{
    public GameObject aoeSphere;
    public GameObject aoeProjectile;  //AOE生成的物体

    public override void Start()
    {
        timer = 10;
        base.Start();
    }

    public override IEnumerator Release()
    {
        if (timer >= coolDown)
        {
            timer = 0;
            int dirCount = 12;
            while (dirCount != 0)
            {
                aoeSphere.transform.RotateAround(this.transform.position, Vector3.up, 30);
                dirCount--;
                Instantiate(aoeProjectile, aoeSphere.transform.position, aoeSphere.transform.rotation);
                yield return new WaitForFixedUpdate();
            }
            StartCoroutine(StartCoolDown());
        }
    }
}
