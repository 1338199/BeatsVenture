using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenSkills : PlayerSkills
{
    public GameObject frozenParicles;

    public float continuousTime = 2f;  //持续时间

    private bool isSelectTarget = false;
    private LayerMask enemyLayer;
    private Renderer hitRender;

    public override void Start()
    {
        timer = 7;
        base.Start();
        enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
    }

    public override IEnumerator Release()
    {
        if (timer >= coolDown)
        {
            while (!isSelectTarget)
            {
                base.SetRange();  //显示施法范围

                ShowIndicator(hitRender);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, 100, enemyLayer))
                {
                    if (hitInfo.transform.tag == "Enemy")
                    {
                        hitRender = hitInfo.transform.GetComponentInChildren<Renderer>();

                        if (Input.GetMouseButtonDown(0))
                        {
                            //显示冰冻效果
                            frozenParicles.SetActive(true);
                            Instantiate(frozenParicles, hitRender.transform.position, Quaternion.identity);

                            //冰冻
                            EnemyController enemyController = hitInfo.transform.GetComponent<EnemyController>();
                            enemyController.enabled = false;
                            float orginSpeed = enemyController.speed;

                            StartCoroutine(RecoverSpeed(enemyController));

                            hitRender = null;
                            ShowIndicator(hitRender);
                            isSelectTarget = true;
                        }
                    }
                }
                else       //没检测到enemy
                {
                    hitRender = null;
                }
                yield return null;
            }
            lineRenderer.positionCount = 0;
            StartCoroutine(StartCoolDown());
        }

    }

    public override IEnumerator StartCoolDown()
    {
        isSelectTarget = false;
        return base.StartCoolDown();
    }

    public override void ShowIndicator(Renderer r)
    {
        if (r == null)
        {
            indicator.SetActive(false);
            return;
        }
        else
        {
            if (CheckIsInRange(hitRender.transform.position, this.transform.position))
            {
                float x = hitRender.bounds.size.x * 0.2f;
                float y = hitRender.bounds.size.z * 0.2f;

                indicator.SetActive(true);
                Vector3 pos = r.transform.position;
                pos.y = 0.1f;
                indicator.transform.position = pos;
                indicator.transform.localScale = new Vector3(x, y, 0);
            }

        }
    }


    private IEnumerator RecoverSpeed(EnemyController enemyController)
    {
        float timer = 0;
        while (timer <= continuousTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        enemyController.enabled = true;
    }
}
