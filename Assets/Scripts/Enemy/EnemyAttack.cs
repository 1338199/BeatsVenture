using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 5;
    private EnemyController enemyController;
    private ParticleSystem ps;
    private bool isStay = false;

    private float moveTimer = 0f;
    public int beatCanAttack = 5;
    private int beat = 0;
    private GameObject mplayer;


    // Use this for initialization
    void Start()
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
        moveTimer += Time.deltaTime;

        float timeOffset = Mathf.Abs(moveTimer - MusicController.getInstance().BeatTime);
        if(timeOffset < 0.1f)
        {
            beat++;
            if(beat >= beatCanAttack && enemyController.isHitPlayer)
            {
                Attack(mplayer);
                beat = 0;
            }
            moveTimer = 0;
        }
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

    protected void Attack(GameObject other)
    {
        ps.Play();
        other.GetComponent<HealthController>().TakeDamage(damage);

    }

    //protected IEnumerator Attack(Collider other)
    //{
    //    while (isStay)
    //    {
    //        ps.Play();

    //        yield return new WaitForSeconds(2.0f);
    //    }
    //}

}
