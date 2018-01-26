using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour, IPlayableCharacter
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
        Movement();
    }

    private void Movement()
    {
        if (!isSelected)
            return;

        //TODO Movements
    }

    private void ReturnToPlayer()
    {
        isSelected = false;
        playableCharacter.Select();
        cameraControl.LookAt(player);
    }
}
