using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 5;
    protected EnemyController enemyController;
    protected ParticleSystem ps;
    protected bool isStay = false;

    protected float moveTimer = 0f;
    protected int beatCanAttack = 1;
    protected int beat = 0;
    protected GameObject mplayer;


    // Use this for initialization
    protected virtual void Start()
    {
        ps = GetComponent<ParticleSystem>();
        enemyController = this.transform.parent.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixedUpdate()
    {
        //if (isStay)
        //{
        //    moveTimer += Time.deltaTime;

        //    float timeOffset = Mathf.Abs(moveTimer - MusicController.getInstance().BeatTime);
        //    if (timeOffset < 0.1f)
        //    {
        //        beat++;
        //        if (!enemyController.isDie && beat >= beatCanAttack && enemyController.isHitPlayer)
        //        {
        //            Attack(mplayer);
        //            beat = 0;
        //        }
        //        moveTimer = 0;
        //    }
        //}
        //else
        //{
        //    moveTimer = 0;
        //    beat = 0;
        //}
        
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            Debug.Log("Can attack");
            enemyController.stopMove();
            isStay = true;
            enemyController.isHitPlayer = true;
            mplayer = other.transform.gameObject;
            //StartCoroutine(Attack(other));

        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            isStay = false;
            enemyController.isHitPlayer = false;
        }
    }

    public virtual void Attack(GameObject other)
    {
        ps.Play();
        other.GetComponent<HealthController>().TakeDamage(damage);

    }

    protected virtual IEnumerator AttackEffect(GameObject other)
    {
        

        yield return new WaitForSeconds(2.0f);
    }

    public int getCanAttack()
    {
        return this.beatCanAttack;
    }

}
