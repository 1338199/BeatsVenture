using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 闪现
/// </summary>
public class FlashSkills : PlayerSkills
{

    public GameObject portalParticleGO;   //传送门粒子

    private bool isFlash = false;
    private LayerMask layer;

    public override void Start()
    {
        timer = 5;
        layer = 1 << LayerMask.NameToLayer("Ground");
        base.Start();
    }

    public override IEnumerator Release()
    {
        if (timer >= coolDown)
        {
            while (!isFlash)
            {
                SetRange();  //显示施法范围
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, 100, layer))
                {
                    if (CheckIsInRange(hitInfo.point, this.transform.position))
                    {
                        portalParticleGO.gameObject.SetActive(true);
                        //显示传送门
                        portalParticleGO.transform.position = hitInfo.point;

                        if (Input.GetMouseButtonDown(0))
                        {
                            if (hitInfo.transform.tag == "Ground")
                            {
                                this.transform.parent.position = new Vector3(portalParticleGO.transform.position.x, 0, portalParticleGO.transform.position.z);
                                portalParticleGO.gameObject.SetActive(false);
                                isFlash = true;

                            }
                        }
                    }
                    else
                    {
                        portalParticleGO.gameObject.SetActive(false);
                    }
                }
                yield return new WaitForFixedUpdate();
            }
            //消除划线
            lineRenderer.positionCount = 0;
            StartCoroutine(StartCoolDown());
        }
    }

    public override IEnumerator StartCoolDown()
    {
        isFlash = false;
        return base.StartCoolDown();
    }
}
