using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BeatIconController : MonoBehaviour
{
    public GameObject Blackhole;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(moveToBlackhole(time));
    }

    // Update is called once per frame
    void Update()
    {
        //var temp = this.GetComponent<RectTransform>().position;
        //this.GetComponent<RectTransform>().position = new Vector3(temp.x-3, temp.y, 0);
    }

    private IEnumerator moveToBlackhole(float totalTime)
    {
        //Debug.Log("start");
        float start_time = Time.time;
        Vector3 startPostion = this.transform.position;
        Vector3 endPosition = Blackhole.transform.position;
        float frac = 0;
        while (frac <= 1)
        {
            frac = (Time.time - start_time) / totalTime;
            transform.position = Vector3.Lerp(startPostion, endPosition, frac);
            yield return null;
        }
        Destroy(gameObject);
    }
}
