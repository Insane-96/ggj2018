using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour,IDoorOpen {

    Quaternion finishRotation;

    private bool isOpening;

    bool IDoorOpen.isOpening
    {
        get
        {
            return isOpening;
        }

        set
        {
            isOpening = value;
        }
    }

    // Use this for initialization
    void Start () {
        finishRotation = new Quaternion(0, -1f, 0, 1);
    }

	// Update is called once per frame
	void Update () {
        if (isOpening) OpenDoor();
        if (transform.rotation == finishRotation)
        {
            isOpening = false;
        }    
    }

    void OpenDoor()
    { 
        transform.rotation = Quaternion.Lerp(transform.rotation, finishRotation, Time.deltaTime * 0.3f);
    }
}
