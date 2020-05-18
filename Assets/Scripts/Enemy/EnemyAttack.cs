using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 5;
    private EnemyController enemyController;
    private ParticleSystem ps;
    private bool isStay = false;
   

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


    private void OnTriggerEnter(Collider other)
    {
        
        if(other.transform.tag == "Player")
        {
            isStay = true;
            enemyController.isHitPlayer = true;
            StartCoroutine(Attack(other));

        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            ps.Stop();
            isStay = false;
            enemyController.isHitPlayer = false;
            

        }
    }

    protected IEnumerator Attack(Collider other)
    {
        while (isStay)
        {
            ps.Play();
            
            yield return new WaitForSeconds(2.0f);
        }
    }

}
