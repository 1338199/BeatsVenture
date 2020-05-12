using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damageAmount = 5;
    public float attackCoolDown; //wait to adpat to the beat
    private EnemyController enemyController;
    private bool isStay = false;

    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        Debug.Log(ps);
        enemyController = this.transform.parent.GetComponent<EnemyController>();
        Debug.Log(enemyController);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.tag);
        enemyController.isHitPlayer = true;
        Debug.Log("Start");
        //wait to change
        if (other.transform.tag == "Player")
        {
            isStay = true;
            StartCoroutine(Attack(other));
        }
            
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Stay");
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("???");
            isStay = false;
            ps.Stop();
            enemyController.isHitPlayer = false;
        }
    }

    protected IEnumerator Attack(Collider other)
    {
        while (isStay)
        {
            Debug.Log("Play");
            ps.Play();
            yield return new WaitForSeconds(2.0f);
        }
        
    }
}
