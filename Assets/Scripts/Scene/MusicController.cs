using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.XR.WSA.Input;

public class MusicController
{
    private static MusicController _instance;
    public static MusicController getInstance()
    {
        if (_instance == null)
        {
            _instance = new MusicController();
        }
        return _instance;
    }

    public void Play()
    {
        var audioObject = new GameObject("backtrack");
        this.audioPlayer = audioObject.AddComponent<AudioSource>();
        audioPlayer.clip = Resources.Load<AudioClip>("Music/" + SongName);
        audioPlayer.playOnAwake = false;
        audioPlayer.loop = true;
        //for (var i = 0; i != beatsPerBar; ++i)
        //{
        //    rhythm.Add(new List<int>(new int[] { 1, 0 }));
        //}
        rhythm.Add(new List<int>(new int[] { 1, 0 }));
        rhythm.Add(new List<int>(new int[] { 1, 0 }));
        rhythm.Add(new List<int>(new int[] { 2, 0, 1 }));
        rhythm.Add(new List<int>(new int[] { 1, 0 }));
        audioPlayer.Play();
    }


    /// <summary>
    /// check if time is legitimate
    /// </summary>
    /// <param name="time">time since game start in seconds. Time.time recommended</param>
    /// <param name="stamp">a number uniquely corresponding to a beat</param>
    /// <returns></returns>
    public bool CheckTime(float time,  int stamp = -1)
    {
        float timeSpan = this.audioPlayer.time;
        float beatCount = timeSpan / BeatTime;
        int beatInBar = (int)beatCount % beatsPerBar;
        var pattern = rhythm[beatInBar];
        float res = beatCount - (int)beatCount;
        Debug.Log(beatInBar + res);
        for (var i = 1; i != pattern.Count; ++i)
        {
            if (Math.Abs(res - (float)pattern[i] / pattern[0]) < thresh)
            {
                stamp = (int)beatCount * 10 + i;
                return true;
            }
        }
        //start of next bar
        int nextBar = ((int)beatCount + 1) % beatsPerBar;
        pattern = rhythm[nextBar];
        if (Math.Abs(res - (1 + pattern[1] / pattern[0])) < thresh)
        {
            stamp = ((int)beatCount + 1) * 10 + 1;
            return true;
        }
        stamp = -1;
        return false;
    }

    private string _songName = "mygame1";
    public string SongName
    {
        get
        {
            return _songName;
        }
        set
        {
            _songName = value;
        }
    }

    private int beatsPerBar = 4;
    private int bpm
    {
        get
        {
            switch (this._songName)
            {
                case "mygame1":
                    return 130;
                    break;
                case "NoEscape":
                    return 119;
                    break;
                default:
                    return 119;
                    break;
            }
        }
    }
    private float thresh = 0.2f;
    private AudioSource audioPlayer;
    private float BarTime
    {
        get
        {
            return 60f / bpm * beatsPerBar;
        }
    }
    public float BeatTime
    {
        get
        {
            return 60f / bpm;
        }
    }
    private List<List<int>> rhythm = new List<List<int>>();

    private MusicController()
    {

    }
}
