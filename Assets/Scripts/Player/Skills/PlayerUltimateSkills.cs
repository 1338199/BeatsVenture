using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateSkills : PlayerSkills
{
    public GameObject meteorPS;
    public float continuousTime = 3;


    public override void Start()
    {
        timer = 10;
        base.Start();
    }


    public override IEnumerator Release()
    {
        if (timer >= coolDown)
        {
            base.SetRange();
            yield return new WaitForSeconds(0.5f);
            base.lineRenderer.positionCount = 0;

            Vector3[] pos =
              {
                    new Vector3(this.transform.position.x + skillRange/2, 8, this.transform.position.z),
                    new Vector3(this.transform.position.x, 8, this.transform.position.z - skillRange/2),
                    new Vector3(this.transform.position.x - skillRange/2, 8, this.transform.position.z),
                    new Vector3(this.transform.position.x , 8, this.transform.position.z + skillRange/2),
             };



            for (int i = 0; i < 4; i++)
            {
                Instantiate(meteorPS, pos[i], meteorPS.transform.rotation);
                yield return null;
            }

            StartCoroutine(StartCoolDown());
        }
    }

    public override IEnumerator StartCoolDown()
    {
        return base.StartCoolDown();
    }


}
