using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts;
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

    private GameObject bulletClone;
    private bool shot;
    private ParabolaPath invisiblePath;
    private GameObject ammo;
    private GameObject canon;
    public GameObject bombIndicator;
    private GameObject bombIndicatorClone;
    public AudioSource fireAudio;

    void Start()
    {
        shot = false;
        /*path = new ParabolaPath(transform.position, player.transform.position, height, gravity);
        path.isClampStartEnd = true;
        transform.LookAt(path.GetPosition(path.time + Time.deltaTime));*/
    }

    void OnDestroy(){
        DestroyImmediate(bombIndicatorClone, true);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!GameController.enemyCanMove)
        {
            return;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        ammo = GameObject.Find("Canon_Spout");
        canon = GameObject.Find("canon");
        rotatingbase.transform.LookAt(player.transform);
        rotatingbase.transform.Rotate(Vector3.up * 90);
        invisiblePath = new ParabolaPath(canon.transform.position, player.transform.position, height, gravity);
        invisiblePath.isClampStartEnd = true;
        canon.transform.LookAt(invisiblePath.GetPosition(invisiblePath.time + Time.deltaTime));
        canon.transform.Rotate(Vector3.left * 90);
        canon.transform.Rotate(Vector3.up * 90);
        if (!shot)
        {
            if (Vector3.Distance(player.transform.position, rotatingbase.transform.position) < range)
            {
                bulletClone = Instantiate(bullet) as GameObject;
                bulletClone.transform.position = ammo.transform.position;
                bulletClone.transform.rotation = rotatingbase.transform.rotation;
                path = new ParabolaPath(bullet.transform.position, player.transform.position, height, gravity);
                path.isClampStartEnd = true;

                shot = true;
                bombIndicatorClone = Instantiate(bombIndicator) as GameObject;
                bombIndicatorClone.transform.position = player.transform.position;
                bombIndicatorClone.transform.rotation = Quaternion.identity;
                bombIndicatorClone.GetComponent<Renderer>().material.SetFloat("_ColorMask", 3f);
                fireAudio.Play();
            }
        }
        else
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

