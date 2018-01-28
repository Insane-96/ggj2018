using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour,IDoor {

    float timer;

    Quaternion DoorClosed;

    Quaternion DoorOpened;

    // Use this for initialization
    void Start () {
        DoorClosed = transform.rotation;
        DoorOpened = new Quaternion(0, -1, 0, 1);

        timer = 0;
    }

	// Update is called once per frame
	void Update () {

        if(timer > 0)
        {
           timer -= Time.deltaTime;
           transform.rotation = Quaternion.Lerp(transform.rotation, DoorOpened, Time.deltaTime * 2f);                    
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, DoorClosed, Time.deltaTime * 2f);
        }   
    }

    void IDoor.OpenDoor()
    {
        timer = 5;
    }
}
