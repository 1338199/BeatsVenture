using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.WSA.Input;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Transform musszleTrans;
    public Boolean debug = false;
    public GameObject bullet;

    public MusicController mc;

    public GameObject playerHalo;

    private float currentTime = 0;   //用来记录时间

    private Animator anim;

    private SkillsController SkillsController;
    private Rigidbody rb;
    public float step
    {
        get
        {
            if (debug)
            {
                return 5f;
            }
            return 0.5f + continuousOnBeatCount/5;
        }
    }

    private int lastRespondedBeat = -1;

    Coroutine moveCorotine = null;

    public int continuousOnBeatCount
    {
        get;
        set;
    } = 0;

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

    private void Update()
    {
        if (move())
            return;
        if (Attack())
            return;
        if (ReleaseSkill())
            return;

    }

    private void startRadiusChange()
    {
        float end = continuousOnBeatCount * 0.05f;
        StartCoroutine(changeRadius(end));
    }
    private IEnumerator changeRadius(float end)
    {
        float start_time = Time.time;
        float frac = 0;
        ParticleSystem.ShapeModule shape = playerHalo.GetComponent<ParticleSystem>().shape;
        float start = shape.radius;
        while (frac <= 1)
        {
            frac = (Time.time - start_time) / 0.2f;
            shape.radius = frac*end + (1-frac)*start;
            yield return null;
        }
    }

    private bool Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (checkOnBeat())
            {
                Lookat();
                this.GetComponent<AudioSource>().Play();
                Instantiate(bullet, musszleTrans.position, musszleTrans.rotation);
                return true;
            }
        }
        return false;
    }

    private bool ReleaseSkill()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (checkOnBeat())
            {
                SkillsController.RealseSkills(1);
                return true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (checkOnBeat())
            {
                SkillsController.RealseSkills(2);
                return true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (checkOnBeat())
            {
                SkillsController.RealseSkills(3);
                return true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (checkOnBeat())
            {
                SkillsController.RealseSkills(4);
                return true;
            }
        }
        return false;
    }


    private bool move()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (checkOnBeat())
            {

                float y = Camera.main.transform.rotation.eulerAngles.y;
                var targetDirection = Quaternion.Euler(0, y, 0) * new Vector3(0, 0, 1);
                Vector3 target = this.transform.position + targetDirection.normalized * step;
                if (CheckCanMove(target - this.transform.position))
                {
                    if (moveCorotine != null)
                    {
                        StopCoroutine(moveCorotine);
                    }

                    moveCorotine = StartCoroutine(SmoothMove(this.transform.position, target));
                    //SmoothMove(this.transform.position, target);
                    return true;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (checkOnBeat())
            {
                float y = Camera.main.transform.rotation.eulerAngles.y;
                var targetDirection = Quaternion.Euler(0, y, 0) * new Vector3(0, 0, -1);
                Vector3 target = this.transform.position + targetDirection.normalized * step;
                if (CheckCanMove(target - this.transform.position))
                {
                    if (moveCorotine != null)
                    {
                        StopCoroutine(moveCorotine);
                    }

                    moveCorotine = StartCoroutine(SmoothMove(this.transform.position, target));
                    //SmoothMove(this.transform.position, target);
                    return true;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (checkOnBeat())
            {
                float y = Camera.main.transform.rotation.eulerAngles.y;
                var targetDirection = Quaternion.Euler(0, y, 0) * new Vector3(-1, 0, 0);
                Vector3 target = this.transform.position + targetDirection.normalized * step;
                if (CheckCanMove(target - this.transform.position))
                {

                    if (moveCorotine != null)
                    {
                        StopCoroutine(moveCorotine);
                    }
                    moveCorotine = StartCoroutine(SmoothMove(this.transform.position, target));
                    //SmoothMove(this.transform.position, target);
                    return true;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (checkOnBeat())
            {
                float y = Camera.main.transform.rotation.eulerAngles.y;
                var targetDirection = Quaternion.Euler(0, y, 0) * new Vector3(1, 0, 0);
                Vector3 target = this.transform.position + targetDirection.normalized * step;
                if (CheckCanMove(target - this.transform.position))
                {

                    if (moveCorotine != null)
                    {
                        StopCoroutine(moveCorotine);
                    }

                    moveCorotine = StartCoroutine(SmoothMove(this.transform.position, target));
                    //  SmoothMove(this.transform.position, target);
                    return true;
                }
            }
        }
        return false;
    }   //控制移动

    private void Lookat()  //控制朝向
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

    private bool CheckCanMove(Vector3 dir)   //判断是否能够进行移动
    {
        Ray ray = new Ray(this.transform.position, dir);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 1.5f))
        {
            if (hitInfo.transform.tag == "Obstacle" || hitInfo.transform.tag == "Enemy")
            {
                anim.SetTrigger("move");
                return false;
            }

            else
            {
                return true;
            }
        }
        else {
            return true;
        }
    }

    private IEnumerator SmoothMove(Vector3 start, Vector3 target)
    {

        anim.SetTrigger("move");
        bool first = true;
        bool canMove = true;
        // while (Vector3.Distance(this.transform.position, target) > 0.001f)
       // Debug.Log((this.transform.position - target).sqrMagnitude);
        while ((this.transform.position - target).sqrMagnitude > 0.1f && canMove)
        {
            Lookat();
           // Debug.Log(target);
            Vector3 direction = target - this.transform.position;
            //direction = direction.normalized;
            Vector3 movement = target;
            Vector3 originMovement = movement;
            Vector3 curPosition = this.transform.position;
            NavMeshHit hit;
            //Debug.Log(first);
            if (first)
            {
                first = false;
                if (NavMesh.SamplePosition(curPosition, out hit, 1f, NavMesh.AllAreas))
                {
                    curPosition = hit.position;
                }
                target = movement;
                if (NavMesh.SamplePosition(target, out hit, 1f, NavMesh.AllAreas))
                {
                    target = hit.position;
                }


                NavMeshPath path = new NavMeshPath();
                if (!NavMesh.CalculatePath(curPosition, target, NavMesh.AllAreas, path))
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
                        target = curPosition + movement * dot;
                        if ((originMovement - target).sqrMagnitude > 0.3f)
                        {
                            target = curPosition;
                        }
                        //Debug.Log(target);
                        //Debug.Log(curPosition);
                        this.transform.position = Vector3.MoveTowards(curPosition, target, speed * Time.deltaTime * step);
                        //Debug.Log(this.transform.position);
                    }
                }

                if (!canMove)
                {
                    //todo
                }
            }
            else
            {
                //Debug.Log(first);
                //Debug.Log(curPosition);
                this.transform.position = Vector3.MoveTowards(curPosition, target, speed * Time.deltaTime * step);
                //Debug.Log(this.transform.position);
            }

            yield return null;
        }

    }

    private bool checkOnBeat(bool use = true)
    {
        var temp = -1;
        if (mc.CheckTime(out temp))
        {
            if (temp > lastRespondedBeat)
            {
                if (use)
                {
                    lastRespondedBeat = temp;
                    continuousOnBeatCount++;
                    continuousOnBeatCount = Math.Min(continuousOnBeatCount, 20);
                    startRadiusChange();
                }
                return true;
            }
            return false;
        }
        else
        {
            if (use)
            {
                UIController.Instance.ShowWaring();
                continuousOnBeatCount -= 10;
                continuousOnBeatCount = Math.Max(continuousOnBeatCount, 0);
                startRadiusChange();
            }
            return false;
        }
    }
}
