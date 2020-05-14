using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDriver : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {

    }

    void Start()
    {
        MusicController.getInstance().SongName = "NoEscape";
        MusicController.getInstance().Play();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
