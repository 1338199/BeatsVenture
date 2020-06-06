using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teleportChecker : MonoBehaviour
{
    public int stageNum;
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
            Debug.Log("Go to Next Level!");
            if (stageNum < 3)
            {
                var nextStageName = "Level" + (stageNum+1).ToString();
                Debug.Log(nextStageName);
                SceneManager.LoadScene(nextStageName);
            }
            else
            {
                Debug.Log("全部通关！");
            }

        }
    }

}
