using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Transform musszleTrans;
    public GameObject bullet;


    private float currentTime = 0;   //用来记录时间

    private Animator anim;

    private SkillsController SkillsController;
    private Rigidbody rb;

    public MusicController mc;

    private void Start()
    {
        anim = GetComponent<Animator>();


        SkillsController = GetComponent<SkillsController>();
        rb = GetComponent<Rigidbody>();

        mc = MusicController.getInstance();
    }

    private void FixedUpdate()
    {
        currentTime = Time.time;
        Lookat();
    }

    private int index = -1;
    void Update()
    {
        //int temp = 0;
        //if (MusicController.getInstance().CheckTime1(Time.time, out temp))
        //{
        //    if (temp != index)
        //    {
                //index = temp;
                //this.GetComponent<AudioSource>().Play();
                Move();
                Attack();
                ReleaseSkill();
        //    }
        //}
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mc.CheckTime(currentTime))
            {
                Lookat();
                this.GetComponent<AudioSource>().Play();
                Instantiate(bullet, musszleTrans.position, musszleTrans.rotation);
            }
            else
            {
                //节奏不对
                //UIController.Instance.ShowWaring();
            }
        }
    }

    void ReleaseSkill()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (mc.CheckTime(currentTime))
            {
                SkillsController.RealseSkills(1);
            }
            else
            {
                //节奏不对
                //UIController.Instance.ShowWaring();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (mc.CheckTime(currentTime))
            {
                SkillsController.RealseSkills(2);
            }
            else
            {
                //节奏不对
                //UIController.Instance.ShowWaring();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (mc.CheckTime(currentTime))
            {
                SkillsController.RealseSkills(3);
            }
            else
            {
                //节奏不对
                //UIController.Instance.ShowWaring();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (mc.CheckTime(currentTime))
            {
                SkillsController.RealseSkills(4);
            }
            else
            {
                //节奏不对
                //UIController.Instance.ShowWaring();
            }
        }
    }


    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (mc.CheckTime(currentTime))
            {
                Vector3 target = this.transform.position + new Vector3(0, 0, 1);
                if (CheckCanMove(target - this.transform.position))
                {

                    StartCoroutine(SmoothMove(this.transform.position, target));
                    //SmoothMove(this.transform.position, target);
                }
            }
            else
            {
                //节奏不对
                //UIController.Instance.ShowWaring();
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (mc.CheckTime(currentTime))
            {
                Vector3 target = this.transform.position + new Vector3(0, 0, -1);
                if (CheckCanMove(target - this.transform.position))
                {
                    StartCoroutine(SmoothMove(this.transform.position, target));
                    //SmoothMove(this.transform.position, target);
                }
            }
            else
            {
                //UIController.Instance.ShowWaring();

            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (mc.CheckTime(currentTime))
            {
                Vector3 target = this.transform.position + new Vector3(-1, 0, 0);
                if (CheckCanMove(target - this.transform.position))
                {
                    StartCoroutine(SmoothMove(this.transform.position, target));
                    //SmoothMove(this.transform.position, target);
                }
            }
            else
            {
                //UIController.Instance.ShowWaring();

            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (mc.CheckTime(currentTime))
            {
                Vector3 target = this.transform.position + new Vector3(1, 0, 0);
                if (CheckCanMove(target - this.transform.position))
                {
                    StartCoroutine(SmoothMove(this.transform.position, target));
                  //  SmoothMove(this.transform.position, target);
                }
            }
            else
            {
                //UIController.Instance.ShowWaring();

            }
        }
    }   //控制移动

    void Lookat()  //控制朝向
    {

        Ray lookatRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.Log(lookatRay);
        RaycastHit hitInfo;
        //Debug.Log(LayerMask.NameToLayer("Ground"));
        if (Physics.Raycast(lookatRay, out hitInfo, 100, 1 << LayerMask.NameToLayer("Ground")))
        {
            Vector3 dir = new Vector3(hitInfo.point.x, this.transform.position.y, hitInfo.point.z);

            if (Vector3.Distance(transform.position, dir) < 1)
            {
                return;
            }

            transform.LookAt(dir);
        }

    }

    bool CheckCanMove(Vector3 dir)   //判断是否能够进行移动
    {
        Ray ray = new Ray(this.transform.position, dir);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 1.5f))
        {
            if (hitInfo.transform.tag == "Obstacle")
                return false;
            else return true;
        }
        else return true;
    }

    //void SmoothMove(Vector3 start, Vector3 target)
    //{

    //    anim.SetTrigger("move");
    //    // while (Vector3.Distance(this.transform.position, target) > 0.001f)

    //    Lookat();

    //    //Vector3 direction = target - start;
    //    //Vector3 movement = direction * speed;
    //    //Vector3 curPosition = this.transform.position;
    //    //NavMeshHit hit;
    //    //if(NavMesh.SamplePosition(curPosition,out hit, 0.5f, -1))
    //    //{
    //    //    curPosition = hit.position;
    //    //}
    //    //Vector3 temp = curPosition + movement;
    //    //if(NavMesh.SamplePosition(temp, out hit, 0.5f, -1))
    //    //{
    //    //    temp = hit.position;
    //    //}

    //    //bool canMove = true;
    //    //NavMeshPath path = new NavMeshPath();
    //    //if (!NavMesh.CalculatePath(curPosition, temp, NavMesh.AllAreas, path))
    //    //{
    //    //    canMove = false;
    //    //}
    //    //else
    //    //{
    //    //    if(path.corners.Length < 2)
    //    //    {
    //    //        if(path.corners.Length == 1)
    //    //        {
    //    //            movement = path.corners[0] - curPosition;
    //    //            movement.y = curPosition.y; //?
    //    //        }
    //    //        else
    //    //        {
    //    //            canMove = false;
    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        movement = path.corners[1] - curPosition;
    //    //        movement.y = curPosition.y;
    //    //        Vector3 moveDirec = movement.normalized;
    //    //        float dot = Vector3.Dot(moveDirec, direction.normalized);
    //    //        movement = moveDirec * speed * dot;

    //    //        Debug.Log(movement);    
    //    //        Debug.Log(dot);
    //    //        Debug.Log(curPosition);
    //    //        Debug.Log(movement + curPosition);
    //    //        this.transform.position = Vector3.MoveTowards(curPosition, movement + curPosition, speed * Time.deltaTime);

    //    //    }
    //    //}

    //    //if (!canMove)
    //    //{
    //    //    //todo
    //    //}


    //    Debug.Log(this.transform.position);
    //    this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
    //    Debug.Log(this.transform.position);
    //}



    IEnumerator SmoothMove(Vector3 start, Vector3 target)
    {

        anim.SetTrigger("move");
        bool first = true;
        // while (Vector3.Distance(this.transform.position, target) > 0.001f)
        Debug.Log((this.transform.position - target).sqrMagnitude);
        while ((this.transform.position-target).sqrMagnitude > 0.1f)
        {
            Lookat();
            Debug.Log(target);
            Vector3 direction = target - start;
            Vector3 movement = direction * speed * Time.deltaTime;
            Vector3 curPosition = this.transform.position;
            NavMeshHit hit;
            Debug.Log(first);
            if (first)
            {
                first = false;
                if (NavMesh.SamplePosition(curPosition, out hit, 0.5f, -1))
                {
                    curPosition = hit.position;
                }
                Vector3 temp = curPosition + movement;
                if (NavMesh.SamplePosition(temp, out hit, 0.5f, -1))
                {
                    temp = hit.position;
                }

                bool canMove = true;
                NavMeshPath path = new NavMeshPath();
                if (!NavMesh.CalculatePath(curPosition, temp, NavMesh.AllAreas, path))
                {
                    canMove = false;
                }
                else
                {
                    if (path.corners.Length < 2)
                    {
                        if (path.corners.Length == 1)
                        {
                            movement = path.corners[0] - curPosition;
                            movement.y = 0; //?
                        }
                        else
                        {
                            canMove = false;
                        }
                    }
                    else
                    {
                        movement = path.corners[1] - curPosition;
                        movement.y = 0;
                        Vector3 moveDirec = movement.normalized;
                        float dot = Vector3.Dot(moveDirec, direction.normalized);
                        target = curPosition + moveDirec * speed * dot;
                        Debug.Log(target);
                        Debug.Log(curPosition);
                        this.transform.position = Vector3.MoveTowards(curPosition, target, speed*Time.deltaTime);
                        Debug.Log(this.transform.position);
                    }
                }

                if (!canMove)
                {
                    //todo
                }
            }
            else
            {
                Debug.Log(first);
                Debug.Log(curPosition);
                this.transform.position = Vector3.MoveTowards(curPosition, target, speed* Time.deltaTime);
                Debug.Log(this.transform.position);
            }
            


            //Debug.Log(this.transform.position);
            //this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
            //Debug.Log(this.transform.position);
            yield return null;
        }

    }


}
