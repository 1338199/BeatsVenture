using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{

    public Image coolDownMask;
    public int skillRange = 6;   //施法范围
    public GameObject indicator;

    public float coolDown = 5;

    public int skill2Cnt = 0;
    public int skill3Cnt = 0;
    protected float timer = 0;

    protected LineRenderer lineRenderer; //用来渲染施法范围

    public virtual void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public virtual IEnumerator Release()
    {
        yield return null;
        StartCoroutine(StartCoolDown());
    }

    public virtual IEnumerator StartCoolDown()    //控制节能冷却时间
    {
        timer = 0;
        while (timer < coolDown)
        {
            timer += Time.deltaTime;
            coolDownMask.fillAmount = 1 - (timer / coolDown);
            yield return null;
        }
    }



    protected bool CheckIsInRange(Vector3 a, Vector3 b)
    {
        if (Vector3.Distance(a, b) <= skillRange)
            return true;
        else return false;
    }

    public virtual void ShowIndicator(Renderer r)
    {

    }

    protected void SetRange()
    {
        Vector3 center = this.transform.position;
        int pointAmount = 100;
        float angle = 360f / pointAmount;

        Vector3 forward = this.transform.forward;

        lineRenderer.positionCount = pointAmount + 1;
        for (int i = 0; i <= pointAmount; i++)
        {
            Vector3 pos = Quaternion.Euler(0, angle * i, 0f) * forward * skillRange + center;
            lineRenderer.SetPosition(i, pos);
        }
    }

}
