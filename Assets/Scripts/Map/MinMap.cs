﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinMap:MonoBehaviour
{

    private static MinMap mapInstance;

    public GameObject plane;

    public GameObject planeLeftDown;

    public List<MapData> players = new List<MapData>();

    public Texture mapTexutre;

    private float maxMapRWidth;

    private float maxMapRHeight;

    public bool isDie;


    public int id;


    public static MinMap getInstance()
    {
        if (mapInstance == null)
        {
            mapInstance = FindObjectOfType(typeof(MinMap)) as MinMap;
        }
        return mapInstance;
    }

    private void Awake()
    {
        mapInstance = this;
    }

    // Use this for initialization
    void Start()
    {

        id = 0;
        isDie = false;

        //maxMapRHeight = plane.GetComponent<MeshFilter>().mesh.bounds.size.x;
        //maxMapRWidth = plane.GetComponent<MeshFilter>().mesh.bounds.size.z;

        maxMapRHeight = 1;
        maxMapRWidth = 1;

        float scaleZ = plane.transform.localScale.z;
        maxMapRHeight = maxMapRHeight * scaleZ;
        float scaleX = plane.transform.localScale.x;
        maxMapRWidth = maxMapRWidth * scaleX;
    }

    public int getGloablId()
    {
        return id++;
    }


    public void addCell(MapData data)
    {
        bool temp = false;
        for(int i=0; i < mapInstance.players.Count; i++)
        {
            if(mapInstance.players[i].name == data.name)
            {
                temp = true;
                break;
            }
            
        }
        if (!temp)
        {
            mapInstance.players.Add(data);
        }
    }

    public void removeCell(MapData data)
    {
        if (mapInstance.players.Contains(data))
        {
            mapInstance.players.Remove(data);
        }
        
    }

    public void updatePosition(MapData data)
    {
        for(int i=0;i < mapInstance.players.Count; i++)
        {
            if(mapInstance.players[i].name == data.name)
            {
                mapInstance.players[i].thdPosition = data.thdPosition;
            }
        }
    }

    private void FixedUpdate()
    {
        for(int i=0;i < mapInstance.players.Count; i++)
        {

            mapInstance.players[i].twdPosition.x = Screen.height - 300  + mapTexutre.height / 2 - (Mathf.Abs(mapInstance.players[i].thdPosition.x - planeLeftDown.transform.position.x) / maxMapRWidth * mapTexutre.height / 2);


            //players[i].twdPosition.x = (Mathf.Abs(players[i].thdPosition.x - planeLeftDown.transform.position.x) / maxMapRWidth * mapTexutre.width / 2)
            //                                + (Screen.width - mapTexutre.width / 2);

            //   Debug.Log(players[i].twdPosition.x);
            mapInstance.players[i].twdPosition.y = (Mathf.Abs(mapInstance.players[i].thdPosition.z - planeLeftDown.transform.position.z) / maxMapRHeight * mapTexutre.width / 2)
                                            + (Screen.width - mapTexutre.width / 2 - 20);
            //Debug.Log(players[i].twdPosition.y);
        }
    }

    private void OnGUI()
    {
        if (!mapInstance.isDie)
        {
            GUI.DrawTexture(new Rect(Screen.width - mapTexutre.width / 2 - 20, Screen.height - 300, mapTexutre.width / 2 + 30, mapTexutre.height / 2 + 20), mapTexutre, ScaleMode.StretchToFill, false);
            for (int j = 0; j < mapInstance.players.Count; j++)
            {
                GUI.DrawTexture(new Rect(mapInstance.players[j].twdPosition.y, mapInstance.players[j].twdPosition.x, 15, 15), mapInstance.players[j].icon);
                // GUIUtility.RotateAroundPivot(-90, new Vector2(players[j].twdPosition.x, players[j].twdPosition.y));
            }
        }
    }

    public void setDie()
    {
        mapInstance.isDie = true; 
    }


    public void flushList()
    {
        mapInstance.players.Clear();
    }
}
