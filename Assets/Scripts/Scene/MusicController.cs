using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        startTime = Time.time;
    }

    /// <summary>
    /// check if time is legitimate
    /// </summary>
    /// <param name="time">time since game start in seconds. Time.time recommended</param>
    /// <returns></returns>
    public bool CheckTime(float time)
    {
        float timeSpan = time - this.startTime;
        float beatCount = timeSpan / BeatTime;
        int beatInBar = (int)beatCount % beatsPerBar;
        var pattern = rhythm[beatInBar];
        float res = beatCount - (int)beatCount;
        Debug.Log(beatInBar + res);
        for (var i = 1; i != pattern.Count; ++i)
        {
            if (Math.Abs(res - (float)pattern[i] / pattern[0]) < thresh)
            {
                Debug.Log("YEAH!");
                return true;
            }
        }
        //start of next bar
        int nextBar = ((int)beatCount + 1) % beatsPerBar;
        pattern = rhythm[nextBar];
        if (Math.Abs(res - (1 + pattern[1] / pattern[0])) < thresh)
        {
            return true;
        }
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
    private int bpm = 130;
    private float startTime = 0;
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
