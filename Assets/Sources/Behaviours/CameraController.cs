using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour, ICameraControl
{

    public GameObject currentFollowing;
    private GameObject oldFollowing;
    
    [Tooltip("Seconds in which the lerp will be completed")]
    public float lerpSpeed = 1;
    public float distance = 5;
    public float height = 6;

    private float moving = 0f;

    void Start()
    {
        transform.Rotate(new Vector3(45, 0, 0));
    }

    bool changed = false;

    void Update()
    {
        if (moving > 0f)
        {
            transform.position = Vector3.Lerp(new Vector3(0, height, -distance) + currentFollowing.transform.position, new Vector3(0, height, -distance) + oldFollowing.transform.position, moving);
            moving -= Time.deltaTime / lerpSpeed;
            Debug.Log("moving " + moving);
        }
        else
        {
            transform.position = new Vector3(0, height, -distance) + currentFollowing.transform.position;
        }
    }

    public void LookAt(GameObject gameObject)
    {
        moving = 1.0f;
        oldFollowing = currentFollowing;
        currentFollowing = gameObject;
    }
}
