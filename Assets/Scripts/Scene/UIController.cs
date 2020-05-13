using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject waringImage;
    /// <summary>
    /// 单例
    /// </summary>
    private static UIController instance;

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
}
