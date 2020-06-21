using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkbcController : MonoBehaviour
{
    int originBeat;
    int last = 0;
    public DirectionalLightController directionalLight;
   
    
    // Start is called before the first frame update
    void Start()
    {
        originBeat = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        int tempBeat;
        MusicController.getInstance().CheckTime(out tempBeat);
        if (tempBeat > originBeat && last > 0)
        {
            last--;
            if(last == 0)
            {
                gameObject.SetActive(false);
                directionalLight.ableLight();
    
            }
        }
        originBeat = tempBeat;
    }

    public void setLast()
    {
        last = 15;
        directionalLight.disableLight();
        gameObject.SetActive(true);
    }
}
