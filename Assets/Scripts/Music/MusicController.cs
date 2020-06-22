using Assets.Scripts;
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

    public GameObject ParLight;

    public void Awake()
    {
        _instance = this;
        this.SongName = "NoEscape";
        var audioObject = new GameObject("backtrack");
        this.audioPlayer = audioObject.AddComponent<AudioSource>();
        audioPlayer.clip = Resources.Load<AudioClip>("Music/" + SongName);
        audioPlayer.playOnAwake = false;
        audioPlayer.loop = true;

        float volume = PlayerPrefs.GetFloat("volume", 0.1f);
        audioPlayer.volume = volume;
        rhythm.Add(new List<int>(new int[] { 1, 0 }));
        rhythm.Add(new List<int>(new int[] { 1, 0 }));
        rhythm.Add(new List<int>(new int[] { 1, 0 }));
        //rhythm.Add(new List<int>(new int[] { 2, 0, 1 }));
        rhythm.Add(new List<int>(new int[] { 1, 0 }));
    }

    public void Start()
    {
        this.TimeOkey = false;

        this.timeBias = -3;
        this.nextForeseeBeatCount = 0;
        this.nextForeseeTime = 0;
        this.nextForeseeHitInBeat = 1;

        BeatBlackhole = GameObject.Find("BeatBlackhole");
        Canvas = GameObject.Find("Canvas");

        foreseenHits = new Queue<HitStamp>();

        Invoke("Play", -this.timeBias);
        this.startTime = Time.time;

        GameController.startGuide();
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
    private float timeBias;
    private float startTime;
    private float time
    {
        get
        {
            return Time.time - this.startTime + this.timeBias;
        }
    }

    public void Update()
    {
        if (time + foreseeTime > nextForeseeTime)
        {
            if(nextForeseeBeatCount>=this.guideBeats)
            {
                createBeatIcon();
            }
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
                Debug.Log(1);
            }
            else
            {
                nextForeseeHitInBeat++;
                nextForeseeTime = nextForeseeBeatCount * BeatTime + BeatTime * (float)pattern[nextForeseeHitInBeat] / pattern[0];
                Debug.Log(2);
            }
        }

        if (foreseenHits.Count!=0 && time > foreseenHits.Peek().time - thresh)
        {
            if (foreseenHits.Peek().stamp > this.Stamp)
            {
                this.Stamp = foreseenHits.Peek().stamp;
                if(GameController.guiding)
                    ParLight.GetComponent<ParController>().changeState(this.Stamp);
            }
            if (time > foreseenHits.Peek().time + thresh)
            {
                if(Stamp/10 == this.guideBeats-1)
                {
                    GameController.stopGuide();
                    ParLight.GetComponent<ParController>().stopGuide();
                }
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
        //for (var i = 0; i != beatsPerBar; ++i)
        //{
        //    rhythm.Add(new List<int>(new int[] { 1, 0 }));
        //}
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
    public int guideBeats
    {
        get
        {
            return this.beatsPerBar * 4;
        }
    }
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
