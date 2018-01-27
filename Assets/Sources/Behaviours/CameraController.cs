using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour, ICameraControl {

    public Transform player;

    public float speed;
 
    public float distance;

    public float height;


    // Use this for initialization
    void Start () {

        transform.position = new Vector3(0, height, distance) + player.transform.position;
        transform.LookAt(player);
    }
	
	// Update is called once per frame
	void Update () {
         
    }

    public void LookAt(GameObject gameObject)
    {     
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, height, distance) + gameObject.transform.position, speed * Time.deltaTime);
    }
}
