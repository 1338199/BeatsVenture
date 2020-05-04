using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public bool isFindPlayer = false;  //是否发现player，该值由子物体dectectSphere控制
    public bool isHitPlayer = false;  //由meleeSphere控制

    protected float moveTimer = 0;   //用来控制敌人的行动时机
    public int beatCanMove = 2;   //敌人经过多少个节拍才可以移动
    protected int beatTimer;  //用来标记经过多少个节拍

    public float speed = 5f;

    protected Animator anim;
    protected GameObject player;

    public virtual void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    public virtual void FixedUpdate()   //不用update是因为update调用的事件是不固定的，而fixedupdate是每0.02s执行一次
    {
        moveTimer += Time.deltaTime;  //

        //计算经过的beat
        float timeOffset = Mathf.Abs(moveTimer - MusicController.Instance.beatTime);
        if (timeOffset <= 0.01f)   //两个浮点数不能直接用 == 比较
        {
            beatTimer++;


            if (isFindPlayer && !isHitPlayer)
            {
                Move();
            }

            moveTimer = 0;
        }
    }

    public void SetPlayer(GameObject g)
    {
        player = g;
    }


    protected bool CheckCanMove(Vector3 dir)   //判断是否能够进行移动
    {
        Ray ray = new Ray(this.transform.position, dir);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 1.5f))
        {
            if (hitInfo.transform.tag == "Ostabcle")
                return false;
            else
                return true;
        }
        else
            return true;
    }

    protected void Move()
    {
        if (beatTimer >= beatCanMove)
        {
            Vector3 target = this.transform.position + GetTargetPosition();

            if (CheckCanMove(target - this.transform.position))
            {
                StartCoroutine(SmoothMove(this.transform.position, target));
                beatTimer = 0;
            }
            else
            {
                beatTimer = 0;
            }
        }
    }

    protected void LooaAtPlayer()
    {
        this.transform.LookAt(player.transform);
    }

    protected IEnumerator SmoothMove(Vector3 start, Vector3 target)
    {
        while (Vector3.Distance(this.transform.position, target) > 0.1f)
        {
            anim.SetTrigger("move");
            LooaAtPlayer();
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
            yield return null;
        }

    }

    protected Vector3 GetTargetPosition()  //获得移动的方向
    {
        Vector3 target = player.transform.position - this.transform.position;
        // Debug.Log(target);
        return target.normalized;
    }

}
