using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject waringImage;
    public Text InfoText;
    public GameObject infoBar;
    /// <summary>
    /// 单例
    /// </summary>
    private static UIController instance;

    void Start()
    {
        infoBar.gameObject.SetActive(false);
    }
    
    public static UIController Instance
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
    }

    public void ShowInfo(string str)
    {
        InfoText.text = str;
        infoBar.gameObject.SetActive(true);
        StartCoroutine(TextFade(infoBar));
    }

    public void ShowWaring()
    {
        waringImage.gameObject.SetActive(true);
        StartCoroutine(Fade(waringImage));
    }

    IEnumerator Fade(GameObject g)
    {
        yield return new WaitForSeconds(0.2f);
        g.SetActive(false);
    }
    
    IEnumerator TextFade(GameObject g)
    {
        yield return new WaitForSeconds(2f);
        g.SetActive(false);
    }
}
