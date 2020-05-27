using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DetectSphere : MonoBehaviour
{

    private EnemyController enemyController;
    // Use this for initialization
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
            Debug.Log("Detect player!");
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
