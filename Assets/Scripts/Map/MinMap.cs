using UnityEngine;
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

    private float initW;

    private float initH;


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
        maxMapRHeight = plane.GetComponent<MeshFilter>().mesh.bounds.size.x;
        maxMapRWidth = plane.GetComponent<MeshFilter>().mesh.bounds.size.z;

        float scaleZ = plane.transform.localScale.z;
        maxMapRHeight = maxMapRHeight * scaleZ;
        float scaleX = plane.transform.localScale.x;
        maxMapRWidth = maxMapRWidth * scaleX;
    }


    public void addCell(MapData data)
    {
        bool temp = false;
        for(int i=0; i < players.Count; i++)
        {
            if(players[i].name == data.name)
            {
                temp = true;
                break;
            }
            
        }
        if (!temp)
        {
            players.Add(data);
        }
    }

    public void removeCell(MapData data)
    {
        players.Remove(data);
    }

    public void updatePosition(MapData data)
    {
        for(int i=0;i < players.Count; i++)
        {
            if(players[i].name == data.name)
            {
                players[i].thdPosition = data.thdPosition;
            }
        }
    }

    private void FixedUpdate()
    {
        for(int i=0;i < players.Count; i++)
        {

            players[i].twdPosition.x = 35 + mapTexutre.height / 2 - (Mathf.Abs(players[i].thdPosition.x - planeLeftDown.transform.position.x) / maxMapRWidth * mapTexutre.height / 2);
         

            //players[i].twdPosition.x = (Mathf.Abs(players[i].thdPosition.x - planeLeftDown.transform.position.x) / maxMapRWidth * mapTexutre.width / 2)
            //                                + (Screen.width - mapTexutre.width / 2);

         //   Debug.Log(players[i].twdPosition.x);
            players[i].twdPosition.y = (Mathf.Abs(players[i].thdPosition.z - planeLeftDown.transform.position.z) / maxMapRHeight * mapTexutre.width / 2)
                                            + (Screen.width - mapTexutre.width / 2 - 20);
            Debug.Log(players[i].twdPosition.y);
        }
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width - mapTexutre.width / 2-20, 50, mapTexutre.width / 2, mapTexutre.height / 2), mapTexutre,ScaleMode.StretchToFill,false);
        for (int j = 0; j < players.Count; j++)
        { 
            GUI.DrawTexture(new Rect(players[j].twdPosition.y, players[j].twdPosition.x, 15, 15), players[j].icon);
           // GUIUtility.RotateAroundPivot(-90, new Vector2(players[j].twdPosition.x, players[j].twdPosition.y));
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
