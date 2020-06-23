using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VSCodeEditor;

public class teleportChecker : MonoBehaviour
{
    public int[] pointCount = {0, 3, 3};
    public int stageId;
    public static int getPointNum = 0;
    public DeathMenu winMenu;

    // Start is called before the first frame update
    void Start()
    {
        getPointNum = 0;
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
            if (getPointNum >= pointCount[stageId - 1])
            {
                Debug.Log("Go to Next Level!");
                if (stageId < 3)
                {
                    var nextStageName = "Level" + (stageId + 1).ToString();
                    Debug.Log(nextStageName);
                 //   MinMap.getInstance().flushList();
                    SceneManager.LoadScene(nextStageName);
                    
                }
                else
                {
                    MinMap.getInstance().setDie();
                    winMenu.ShowDeathMenu();
                    Debug.Log("全部通关！");
                }
            }
            else
            {
                Debug.Log("还没有捡到所有的点！");
            }

        }
    }

    public static void GetPoint()
    {
        getPointNum += 1;
        Debug.Log(getPointNum);
    }
    
}