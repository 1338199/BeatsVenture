using UnityEngine;
using System.Collections;

public class MinMapCell : MonoBehaviour
{
    public Texture icon;
    public string name;
    MapData mapData = new MapData();
    bool isOk = true;


    public void Awake()
    {
        mapData.name = name + MinMap.getInstance().getGloablId();
        mapData.icon = icon;
        mapData.thdPosition = this.transform.position;

        MinMap.getInstance().addCell(mapData);
       
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isOk){
            mapData.thdPosition = this.transform.position;
            MinMap.getInstance().updatePosition(mapData);
        }
       
    }

    private void OnDestroy()
    {
        if (isOk)
        {
            RemoveFromMap();
        }
        
    }

    public void RemoveFromMap()
    {
        if (MinMap.getInstance())
        {
            MinMap.getInstance().removeCell(mapData);
            isOk = false;
        }
        
    }
}
