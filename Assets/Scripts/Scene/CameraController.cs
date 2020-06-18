using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;
 
    //-----Publics variables-----\\
    [Header("Variables")]
    public Transform player;
 
    [Space]
    [Header("Position")]
    public float camPosX;
    public float camPosY;
    public float camPosZ;
 
    [Space]
    [Header("Rotation")]
    public float camRotationX;
    public float camRotationY;
    public float camRotationZ;
 
    [Space]
    [Range(0f, 10f)]
    public float turnSpeed;
 
    //-----Privates functions-----\\
    private void Start()
    {
        offset = new Vector3(camPosX,  camPosY, camPosZ);
        transform.rotation = Quaternion.Euler(camRotationX, camRotationY, camRotationZ);
    }
 
 
    private void LateUpdate()
    {
        //offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) *  offset;
        if (Input.GetKey(KeyCode.Q))
        {
            offset = Quaternion.AngleAxis(turnSpeed, Vector3.up) *  offset;
        }else if (Input.GetKey(KeyCode.E))
        {
            offset = Quaternion.AngleAxis(-turnSpeed, Vector3.up) *  offset;
        }
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}
