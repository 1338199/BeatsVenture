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
    public AudioSource audioSource;

    public override void Start()
    {
        timer = 7;
        base.Start();
        enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
    }

    public override IEnumerator Release()
    {
        skill3Cnt++;
        if (timer >= coolDown)
        {
            while (!isSelectTarget && skill3Cnt%2==1)
            {
                base.SetRange();  //显示施法范围

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, 6, enemyLayer))
                {
                    if (hitInfo.transform.parent.tag == "Enemy")
                    {

                        hitRender = hitInfo.transform.parent.GetComponentInChildren<Renderer>();

                        if (Input.GetMouseButtonDown(0))
                        {
                            Debug.Log("Frozen");
                            //显示冰冻效果
                            frozenParicles.SetActive(true);
                            Instantiate(frozenParicles, hitRender.transform.position, Quaternion.identity);

                            //冰冻
                            audioSource.Play();
                            EnemyController enemyController = hitInfo.transform.parent.GetComponent<EnemyController>();
                            enemyController.enabled = false;
                            float orginSpeed = enemyController.speed;

                            StartCoroutine(RecoverSpeed(enemyController));

                            hitRender = null;
                            isSelectTarget = true;
                            skill3Cnt++;
                        }
                    }
                    else       //没检测到enemy
                    {
                        hitRender = null;
                    }

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



    private IEnumerator RecoverSpeed(EnemyController enemyController)
    {
        float timer = 0;
        while (timer <= continuousTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        if (enemyController)
        {
            enemyController.enabled = true;
        }
        
    }
}
