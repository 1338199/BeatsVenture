using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class TurretController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    public GameObject rotatingbase;
    public float height = 16f;
    public float gravity = -9.8f;
    public float range = 10f;
    private ParabolaPath path;
    public GameObject bullet;
    private bool shot;
    private ParabolaPath invisiblePath;
    private GameObject ammo;
    private GameObject canon;
    public GameObject bombIndicator;
    private GameObject bombIndicatorClone;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ammo = GameObject.Find("Canon_Spout");
        canon = GameObject.Find("canon");
        shot = false;
        /*path = new ParabolaPath(transform.position, player.transform.position, height, gravity);
        path.isClampStartEnd = true;
        transform.LookAt(path.GetPosition(path.time + Time.deltaTime));*/
    }



    // Update is called once per frame
    void Update()
    {
        rotatingbase.transform.LookAt(player.transform);
        rotatingbase.transform.Rotate(Vector3.up * 90);
        invisiblePath = new ParabolaPath(canon.transform.position, player.transform.position, height, gravity);
        invisiblePath.isClampStartEnd = true;
        canon.transform.LookAt(invisiblePath.GetPosition(invisiblePath.time + Time.deltaTime));
        canon.transform.Rotate(Vector3.left * 90);
        canon.transform.Rotate(Vector3.up * 90);
        if (!shot)
        {
            if (Vector3.Distance(player.transform.position, rotatingbase.transform.position) < range) {
                Instantiate(bullet, ammo.transform.position, rotatingbase.transform.rotation);
                path = new ParabolaPath(bullet.transform.position, player.transform.position, height, gravity);
                path.isClampStartEnd = true;

                shot = true;
                bombIndicatorClone = Instantiate(bombIndicator) as GameObject;
                bombIndicatorClone.transform.position = player.transform.position;
                bombIndicatorClone.transform.rotation = Quaternion.identity;
                bombIndicatorClone.GetComponent<Renderer>().material.SetFloat("_ColorMask", 3f);
            }
        } else
        {
            float t = Time.deltaTime;
            path.time += t;
            if (path.time >= path.totalTime)
            {
                Destroy(bombIndicatorClone);
                shot = false;
            }
        }
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
          
        }*/
        /*float t = Time.deltaTime;
        path.time += t;
        transform.position = path.position;
        transform.LookAt(path.GetPosition(path.time + t));
        if (path.time >= path.totalTime) enabled = false;*/
    }
}
