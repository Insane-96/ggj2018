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
    public float distance = 10;
    public float height = 6;

    void Start()
    {
        transform.Rotate(new Vector3(45, 0, 0));
    }

    bool changed = false;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, currentFollowing.transform.position - (transform.forward * distance), Time.deltaTime);
    }

    public void LookAt(GameObject gameObject)
    {
        oldFollowing = currentFollowing;
        currentFollowing = gameObject;
    }
}