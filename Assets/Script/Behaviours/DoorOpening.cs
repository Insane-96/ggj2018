using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour,IDoor {

    public enum Facing
    {
        X_NEGATIVE,
        Z_NEGATIVE,
        X_POSITIVE,
        Z_POSITIVE
    }

    float timer;

    public Facing rotation = Facing.X_POSITIVE;

    Quaternion DoorClosed;

    Quaternion DoorOpened;

    // Use this for initialization
    void Start () {
        DoorClosed = transform.rotation;
        Debug.Log(this.gameObject.name + " " + new Vector3(((int)rotation - 2) * 90f, 0, 0));
        DoorOpened = Quaternion.LookRotation(new Vector3(((int)rotation - 2) * 90f, 0, 0));

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
