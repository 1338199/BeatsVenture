using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.AI;

public class BatController : EnemyController
{



    protected Vector3[] hor_direc = { new Vector3(1, 0, 0), new Vector3(-1, 0, 0) };
    protected Vector3[] ver_direc = { new Vector3(0, 0, 1), new Vector3(0, 0, -1) };
    public int select = 1;
    protected Vector3 curDirec;

    // Start is called before the first frame update
    public override void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        enemyAttack = this.GetComponentInChildren<EnemyAttack>();
        agent.speed = 5f;
        agent.stoppingDistance = 1.5f;
        if(select == 1)
        {
            directions = hor_direc;
        }
        else
        {
            directions = ver_direc;
        }
        curDirec = directions[0];
    }

    // Update is called once per frame
    //public override void FixedUpdate()   //不用update是因为update调用的事件是不固定的，而fixedupdate是每0.02s执行一次
    //{
    //    //moveTimer += Time.deltaTime;  //


    //    ////计算经过的beat
    //    //float timeOffset = Mathf.Abs(moveTimer - MusicController.getInstance().BeatTime);
    //    //if (timeOffset <= 0.01f)   //两个浮点数不能直接用 == 比较
    //    //{
    //    //    beatTimer++;

    //    int tempBeat;
    //    MusicController.getInstance().CheckTime(out tempBeat);
    //    if (tempBeat > originBeat)
    //    {
    //        accumulateBeat++;
    //    }
    //    originBeat = tempBeat;


    //    if (!isFindPlayer && accumulateBeat > beatCanMove)
    //    {

    //        if (coroutine != null)
    //        {
    //            StopCoroutine(coroutine);
    //        }

    //        coroutine = StartCoroutine(Patrol());
    //        accumulateBeat = 0;
    //    }
    //    else if (isFindPlayer)
    //    {
            
    //    }

    //}

    private void ChangeDirec()
    {
        if(curDirec == directions[0])
        {
            curDirec = directions[1];
        }
        else
        {
            curDirec = directions[0];
        }
    }

    protected override IEnumerator Patrol()
    {
        
        Vector3 target = this.transform.position + curDirec * step;
        Vector3 target_clone = target;
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(this.transform.position, target, NavMesh.AllAreas, path);
        if (path.corners.Length == 0)
        {
            ChangeDirec();
        }
        else
        {
            Vector3 middlePoint = path.corners[0];
            target_clone.y = middlePoint.y;
            if (path.corners.Length > 1)
            {
                middlePoint = path.corners[1];
            }


            if (Vector3.Distance(middlePoint, target_clone) < 0.01f)
            {
                this.transform.LookAt(target);

                while (Vector3.Distance(this.transform.position, target) > 0.1)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
                    yield return null;
                }
            }
            else
            {
                ChangeDirec();
            }
        }

    }

}
