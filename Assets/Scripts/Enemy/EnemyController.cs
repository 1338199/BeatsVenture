using System;
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
//    protected int beatTimer;  //用来标记经过多少个节拍
    public bool isDie = false;
    public int step = 1;

    protected int originBeat = 0;
    protected int accumulateBeat = 0;

    public float speed = 5f;

    protected Animator anim;
    protected GameObject player;
    protected EnemyAttack enemyAttack;

    protected NavMeshAgent agent;


    protected Vector3[] directions = { new Vector3(1, 0, 0),
        new Vector3(-1,0,0), new Vector3(0,0,1), new Vector3(0,0,-1),
        new Vector3(1,0,1).normalized, new Vector3(-1,0,1).normalized,
        new Vector3(1,0,-1).normalized, new Vector3(-1,0,-1).normalized
    };

    protected Coroutine coroutine = null;

    public virtual void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        enemyAttack = this.GetComponentInChildren<EnemyAttack>();
        agent.speed = 5f;
        agent.stoppingDistance = 1.5f;
        agent.updateRotation = false;
    }

    public virtual void FixedUpdate()   //不用update是因为update调用的事件是不固定的，而fixedupdate是每0.02s执行一次
    {
        
        //moveTimer += Time.deltaTime;  //


        ////计算经过的beat
        //float timeOffset = Mathf.Abs(moveTimer - MusicController.getInstance().BeatTime);
        //if (timeOffset <= 0.01f)   //两个浮点数不能直接用 == 比较
        //{
        //    beatTimer++;

        int tempBeat;
        MusicController.getInstance().CheckTime(out tempBeat);
        if(tempBeat > originBeat)
        {
            accumulateBeat++;
        }
        originBeat = tempBeat;



        if (!isHitPlayer && accumulateBeat > beatCanMove)
        {

            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            if (isFindPlayer && !isHitPlayer)
            {
                coroutine = StartCoroutine(Move());
            }
            else if(!isFindPlayer)
            {
                coroutine = StartCoroutine(Patrol());
            }

            accumulateBeat = 0;
        }
        else if(isHitPlayer && accumulateBeat > enemyAttack.beatCanAttack)
        {
            enemyAttack.Attack(player);
            accumulateBeat = 0;
        }
            

        //    moveTimer = 0;
        //}
    }

    public void SetPlayer(GameObject g)
    {
        player = g;
    }

    protected virtual IEnumerator  Move()
    {
        if(!isHitPlayer)
        {
            agent.isStopped = false;
        }
        //StartCoroutine(Move2Player());
        
        LookAtPlayer();
        //agent.destination = player.transform.position;
        //Debug.Log(agent.path.corners);
        NavMeshPath path = new NavMeshPath();
        Debug.Log(transform.position);
        Debug.Log(player.transform.position);
        NavMesh.CalculatePath(transform.position, player.transform.position, NavMesh.AllAreas, path);
//        Debug.Log(path.corners[path.corners.Length - 1]);
        if(path.corners.Length == 0)
        {

        }
        else
        {
            Vector3 middlePoint = path.corners[1];
            Vector3 mmDistance = middlePoint - this.transform.position;
            Vector3 direc = mmDistance.normalized;
            float p = mmDistance.x / direc.x;
            Vector3 target = mmDistance + this.transform.position;
            if (p > step)
            {
                target = this.transform.position + step * direc;
            }



            while (Vector3.Distance(this.transform.position, target) > 0.1 && !isHitPlayer)
            {
                //   anim.SetTrigger("move");
                this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
                yield return null;
            }
            //Vector3 destination = this.transform.position + agent.speed * direc;
            //agent.SetDestination(path.corners[path.corners.Length-1]);
        }


    }


    protected virtual IEnumerator Patrol()
    {
        int a = UnityEngine.Random.Range(0, directions.Length);
        Vector3 target = this.transform.position + directions[a] * step;
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(this.transform.position, target, NavMesh.AllAreas, path);
        if(path.corners.Length == 0)
        {

        }
        else
        {
            Vector3 middlePoint = path.corners[0];
            if (path.corners.Length > 1)
            {
                middlePoint = path.corners[1];
            }


            if (Vector3.Distance(middlePoint, target) < 0.01f)
            {
                this.transform.LookAt(target);

                while (Vector3.Distance(this.transform.position, target) > 0.1)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
                    yield return null;
                }
            }
        }
        
    }

    protected IEnumerator Move2Player()
    {
        //while(isFindPlayer && Vector3.Distance(this.transform.position,player.transform.position) > 5f)
        //{
     //  anim.SetTrigger("move");
        LookAtPlayer();
        agent.SetDestination(player.transform.position);
        Debug.Log(agent.path.corners);
        yield return null;
        //}
    }


    //protected bool CheckCanMove(Vector3 dir)   //判断是否能够进行移动
    //{
    //    Ray ray = new Ray(this.transform.position, dir);

    //    RaycastHit hitInfo;
    //    if (Physics.Raycast(ray, out hitInfo, 1.5f))
    //    {
    //        if (hitInfo.transform.tag == "Ostabcle")
    //            return false;
    //        else
    //            return true;
    //    }
    //    else
    //        return true;
    //}

    //protected void Move()
    //{
    //    if (beatTimer >= beatCanMove)
    //    {
    //        Vector3 target = this.transform.position + GetTargetPosition();

    //        if (CheckCanMove(target - this.transform.position))
    //        {
    //            StartCoroutine(SmoothMove(this.transform.position, target));
    //            beatTimer = 0;
    //        }
    //        else
    //        {
    //            beatTimer = 0;
    //        }
    //    }
    //}

    public void stopMove()
    {
        //anim.SetTrigger("move");
        if (agent.enabled)
        {
            agent.isStopped = true;
        }
        
    }

    protected void LookAtPlayer()
    {
        this.transform.LookAt(player.transform);
    }

    //protected IEnumerator SmoothMove(Vector3 start, Vector3 target)
    //{
    //    while (Vector3.Distance(this.transform.position, target) > 0.1f)
    //    {
    //        anim.SetTrigger("move");
    //        LooaAtPlayer();
    //        this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
    //        yield return null;
    //    }

    //}

    protected Vector3 GetTargetPosition()  //获得移动的方向
    {
        Vector3 target = player.transform.position - this.transform.position;
        // Debug.Log(target);
        return target.normalized;
    }


    public void disableNav()
    {
        agent.enabled = false;
    }
}
