﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnmovablePatientControl : MonoBehaviour, IPlayableCharacter
{
    private bool isSelected = false;
    public ICameraControl cameraControl;
    public GameObject player;
    private IPlayableCharacter playableCharacter;

    public float movementSpeed;

    public void Select()
    {
        playableCharacter = player.GetComponent<IPlayableCharacter>();
        isSelected = true;
    }

    void Start()
    {

    }

    void Update()
    {
        Scream();
    }

    private void Scream()
    {
        if (!isSelected)
            return;

        //TODO Scream
    }

    private void ReturnToPlayer()
    {
        isSelected = false;
        playableCharacter.Select();
        cameraControl.LookAt(player);
    }
}