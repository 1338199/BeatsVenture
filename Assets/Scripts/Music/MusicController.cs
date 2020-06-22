using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.XR.WSA.Input;
public class MusicController : MonoBehaviour
{
    private static MusicController _instance;
    public static MusicController getInstance()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType(typeof(MusicController)) as MusicController;
        }
        return _instance;
    }

    public Light spotLight;

    public void Awake()
    {
        _instance = this;
        this.SongName = "NoEscape";
        Play();
    }

    public void Start()
    {
        this.TimeOkey = false;

        this.nextForeseeBeatCount = this.WarmUpBars * this.beatsPerBar;
        this.nextForeseeTime = this.nextForeseeBeatCount * this.BeatTime;
        this.nextForeseeHitInBeat = 1;

        BeatBlackhole = GameObject.Find("BeatBlackhole");
        Canvas = GameObject.Find("Canvas");

        foreseenHits = new Queue<HitStamp>();
    }

    private struct HitStamp
    {
        public int stamp;
        public float time;
    }
    private float foreseeTime
    {
        get
        {
            return this.ForeseeBeats * this.BeatTime;
        }
    }
    private Queue<HitStamp> foreseenHits = new Queue<HitStamp>();
    private int nextForeseeBeatCount;
    private int nextForeseeHitInBeat;
    private float nextForeseeTime;

    private int temp = 0;
    public void Update()
    {
        float timeSpan = this.audioPlayer.time;
        if (timeSpan + foreseeTime > nextForeseeTime)
        {
            createBeatIcon();
            foreseenHits.Enqueue(new HitStamp
            {
                time = nextForeseeTime,
                stamp = nextForeseeBeatCount * 10 + nextForeseeHitInBeat
            });
            int beatInBar = (int)nextForeseeBeatCount % beatsPerBar;
            var pattern = rhythm[beatInBar];
            if (nextForeseeHitInBeat + 1 > pattern.Count - 1)
            {
                nextForeseeBeatCount++;
                nextForeseeHitInBeat = 1;
                nextForeseeTime = BeatTime * nextForeseeBeatCount;
            }
            else
            {
                nextForeseeHitInBeat++;
                nextForeseeTime = nextForeseeBeatCount * BeatTime + BeatTime * (float)pattern[nextForeseeHitInBeat] / pattern[0];
            }
        }

        if (foreseenHits.Count!=0 && timeSpan > foreseenHits.Peek().time - thresh)
        {
            if (foreseenHits.Peek().stamp > this.Stamp)
            {
                //var Colors = new Color[] { Color.red, Color.yellow, Color.white};
                //spotLight.color = Colors[temp % Colors.Length];
                //temp++;
                this.Stamp = foreseenHits.Peek().stamp;
                //spotLight.enabled = !spotLight.enabled;
            }
            if (timeSpan > foreseenHits.Peek().time + thresh)
            {
                foreseenHits.Dequeue();
                TimeOkey = false;
            }
            else
            {
                TimeOkey = true;
            }
        }
    }

    public void lateUpdate()
    {

    }

    private AudioSource audioPlayer;
    public void Play()
    {
        var audioObject = new GameObject("backtrack");
        this.audioPlayer = audioObject.AddComponent<AudioSource>();
        audioPlayer.clip = Resources.Load<AudioClip>("Music/" + SongName);
        audioPlayer.playOnAwake = false;
        audioPlayer.loop = true;

        float volume = PlayerPrefs.GetFloat("volume", 0.1f);
        audioPlayer.volume = volume;
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

    public void Pause()
    {
        this.audioPlayer.Pause();
    }


    private bool TimeOkey { get; set; }
    private int Stamp { get; set; }
    /// <summary>
    /// check if time is legitimate
    /// </summary>
    /// <param name="stamp">a number uniquely corresponding to a beat</param>
    /// <returns></returns>
    public bool CheckTime(out int stamp)
    {
        stamp = this.Stamp;
        return TimeOkey;
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
    public int ForeseeBeats = 2;
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
    public float thresh = 0.2f;
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

    public int WarmUpBars
    {
        get;
        set;
    } = 1;
    private List<List<int>> rhythm = new List<List<int>>();

    public UnityEngine.GameObject BeatIcon;
    public GameObject BeatBlackhole;
    public GameObject Canvas;
    private void createBeatIcon()
    {
        var icon = GameObject.Instantiate(BeatIcon);
        icon.transform.SetParent(Canvas.transform, false);
        icon.GetComponent<BeatIconController>().time = foreseeTime;
        icon.GetComponent<BeatIconController>().Blackhole = BeatBlackhole;
    }
}
