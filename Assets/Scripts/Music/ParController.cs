using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ParController : MonoBehaviour
{
    public GameObject Player;
    public GameObject ParLeft1;
    public GameObject ParRight1;
    public GameObject ParLeft2;
    public GameObject ParRight2;
    public GameObject ParLeft3;
    public GameObject ParRight3;
    public bool activate
    {
        get
        {
            return gameObject.activeSelf;
        }
    }

    private GameObject[] lights;
    List<GameObject[]> lightsInGroup;

    public void Awake()
    {
        this.lights = new GameObject[] { ParLeft1, ParLeft2, ParLeft3, ParRight1, ParRight2, ParRight3 };
        this.lightsInGroup = new List<GameObject[]>();
        lightsInGroup.Add(new GameObject[] { ParLeft1, ParLeft1 });
        lightsInGroup.Add(new GameObject[] { ParRight1, ParRight1 });
        lightsInGroup.Add(new GameObject[] { ParLeft2, ParRight2 });
        lightsInGroup.Add(new GameObject[] { ParLeft3, ParLeft3 });
        lightsInGroup.Add(new GameObject[] { ParLeft1, ParRight1 });
        lightsInGroup.Add(new GameObject[] { ParLeft1, ParRight1 });
        lightsInGroup.Add(new GameObject[] { ParRight3, ParRight3 });
    }
    public void Start()
    {
        float y = gameObject.transform.position.y;
        var playerPostion = Player.transform.position;
        gameObject.transform.position = new Vector3(playerPostion.x, y, playerPostion.z);
    }

    private bool firstTime = true;
    private int change_index = 0;
    private Color[] colors = new Color[] { new Color(255, 154, 0), new Color(255, 70, 30, 100), new Color(255, 92, 0) };
    public void changeState()
    {
        for (int i = 0; i != this.lights.Length; ++i)
        {
            lights[i].GetComponent<AreaLight>().m_Color = colors[change_index % colors.Length];
            lights[i].SetActive(false);
        }
        lightsInGroup[change_index % lightsInGroup.Count][0].SetActive(true);
        lightsInGroup[change_index % lightsInGroup.Count][1].SetActive(true);
        change_index++;
        //changeColor();
    }

    public void switchPar()
    {
        if (firstTime)
        {
            firstTime = false;
            return;
        }
        gameObject.SetActive(!activate);
        if(activate)
        {
            changeState();
        }
    }



    public void stopGuide()
    {
        gameObject.SetActive(true);
        for (int i = 0; i != this.lights.Length; ++i)
        {
            lights[i].SetActive(false);
        }

    }
    public void startFlush()
    {
        gameObject.SetActive(true);
        StartCoroutine(flush(4));
    }

    private IEnumerator flush(int flushes)
    {
        var TimeSpan = MusicController.getInstance().BeatTime / flushes;
        var nextFlshTime = Time.time;
        while(flushes != 0)
        {
            if (Time.time > nextFlshTime)
            {
                nextFlshTime += TimeSpan;
                changeState();
                flushes--;
            }
            yield return null;
        }
    }
}