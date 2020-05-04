using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip audioClip;

    public float beatTime = 0;
    public int bpm;

    public float timeDeviation = 0.1f;  //允许玩家按键的时间偏差量

    public float minCanMoveTime;  //用来表示可以进行操作的最小时间（两次节奏的时间间隔）
    public float maxCanMoveTime;//用来表示可以进行操作的最大时间（两次节奏的时间间隔）

    private AudioSource audioSource;


    /// <summary>
    /// 单例
    /// </summary>
    private static MusicController instance;

    public static MusicController Instance
    {
        get
        {
            return instance;
        }

    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        beatTime = 60f / bpm;
        //Debug.Log(beatTime);
        minCanMoveTime = beatTime - timeDeviation;
        maxCanMoveTime = beatTime + timeDeviation;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
    }
}
