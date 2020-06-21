using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class BlackholeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startVibrate(float timeSpan)
    {
        StartCoroutine(BlackholeVibrate(timeSpan));
    }

    private IEnumerator BlackholeVibrate(float timeSpan)
    {
        var startTime = Time.time;
        float frac = 0;
        var startScale = new Vector3(1f, 1f, 1f);
        var maxScale = new Vector3(1.5f, 1.5f, 1.5f);
        var blackhole = this.gameObject;
        while (frac < 1)
        {
            frac = (Time.time - startTime) / timeSpan;
            if (frac < 0.5)
            {
                frac *= 2;
                blackhole.transform.localScale = Vector3.Lerp(startScale, maxScale, frac);
            }
            else
            {
                frac = (frac - 0.5f) * 2f;
                blackhole.transform.localScale = Vector3.Lerp(maxScale, startScale, frac);
            }
            yield return null;
        }
    }
}
