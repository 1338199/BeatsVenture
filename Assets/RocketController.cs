using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    // Start is called before the first frame update
    private ParabolaPath path;
    private GameObject player;
    public float height = 16f;
    public float gravity = -9.8f;
    public GameObject bombIndicator;
    public GameObject fx;
    static GameObject fxClone;
    private Vector3 aim;
    public AudioSource globalExplosionAudio;
  
    
    void Start()
    {
        globalExplosionAudio = GameObject.Find("/ExplosionAudio").GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        path = new ParabolaPath(this.transform.position, player.transform.position, height, gravity);
        aim = player.transform.position;
        path.isClampStartEnd = true;
        transform.LookAt(path.GetPosition(path.time + Time.deltaTime));
        Destroy(fxClone);
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.deltaTime;
        path.time += t;
        transform.position = path.position;

        // 计算转向
        transform.LookAt(path.GetPosition(path.time + t));
        transform.Rotate(Vector3.right * 90);

        // 简单模拟一下碰撞检测
        if (path.time >= path.totalTime)
        {
            globalExplosionAudio.Play();
            fxClone = Instantiate(fx) as GameObject;
            fxClone.transform.position = path.GetPosition(path.totalTime);
            fxClone.transform.position = fxClone.transform.position + Vector3.up*0.3f;
            fxClone.transform.localScale = fxClone.transform.localScale * 3;
            fxClone.transform.rotation = Quaternion.identity;
            fxClone.transform.Rotate(Vector3.right * 90);
            float range = bombIndicator.GetComponent<MeshFilter>().sharedMesh.bounds.size.x * bombIndicator.transform.localScale.x/2;
            float dis = Vector3.Distance(player.transform.position, aim) ;
            if (dis < range)
            {
                player.GetComponent<HealthController>().TakeDamage(10);
            }
            DestroyImmediate(gameObject, true); 
        }
    }
}
