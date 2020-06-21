using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class GameController
    {
        static private AudioListener listener;
        static private void awake()
        {
            listener = GameObject.Find("MusicController").GetComponent<AudioListener>();
        }

        static public bool paused
        {
            get;
            set;
        } = false;

        static public void pauseGame()
        {
            paused = true;
            AudioListener.pause = true;
            Time.timeScale = 0;
        }

        static public void resumeGame()
        {
            paused = false;
            AudioListener.pause = false;
            Time.timeScale = 1;
        }
    }
}
