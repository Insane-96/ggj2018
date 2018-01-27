using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour,IDoor {

    Quaternion finishRotation;

    Quaternion startRotation;

    bool isOpening;

    float timeToClose;

    void Awake()
    {
        startRotation = transform.rotation;
    }

    // Use this for initialization
    void Start () {
  
        finishRotation = new Quaternion(0, -1f, 0, 1);
    }

	// Update is called once per frame
	void Update () {
 
        if(isOpening)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, finishRotation, Time.deltaTime * 0.3f);
            timeToClose = 5f;
        }
        if (transform.rotation == finishRotation)
        {
            isOpening = false;
            timeToClose -= Time.deltaTime;
        }

        if(!isOpening && timeToClose <= 0)
        {      
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotation, Time.deltaTime * 0.5f);
            if (transform.rotation == startRotation) timeToClose = 5f;
        }
    }

    void IDoor.OpenDoor()
    {
        isOpening = true;
    }
}
