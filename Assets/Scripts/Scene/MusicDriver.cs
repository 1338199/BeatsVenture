using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDriver : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        MusicController.getInstance().SongName = "mygame01";
        MusicController.getInstance().Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
