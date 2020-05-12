using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSphere : MonoBehaviour
{
    private EnemyController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = this.transform.parent.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {   

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("11111");
            enemyController.isFindPlayer = true;
            enemyController.SetPlayer(other.transform.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            enemyController.isFindPlayer = false;
        }
    }
}
