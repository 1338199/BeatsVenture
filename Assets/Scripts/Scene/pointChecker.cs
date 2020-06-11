using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VSCodeEditor;

public class pointChecker : MonoBehaviour
{
    public int id;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider collider)
    {
        var objectName = collider.name;
        if (objectName == "Player")
        {
            Debug.Log("Get Point!");
            //Destroy(this);
            Destroy(this.gameObject);
            teleportChecker.GetPoint();
        }
    }
}