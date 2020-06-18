using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("5Key"))
        {
            cam1.GetComponent<Camera>().enabled = true;
            cam1.tag = "MainCamera";
            cam2.GetComponent<Camera>().enabled = false;
            cam2.tag = "Untagged";
        }
        if (Input.GetButtonDown("6Key"))
        {
            cam2.GetComponent<Camera>().enabled = true;
            cam2.tag = "MainCamera";
            cam1.GetComponent<Camera>().enabled = false;
            cam1.tag = "Untagged";
        }
    }
}
