using System.Collections;
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
        cameraControl.LookAt(this.gameObject);
        isSelected = true;
    }

    void Start()
    {
        cameraControl = Camera.main.GetComponent<ICameraControl>();
    }

    void Update()
    {
        Scream();
        ReturnToPlayer();
    }

    private void Scream()
    {
        if (!isSelected)
            return;

        //TODO Scream
    }

    private void ReturnToPlayer()
    {
        if (Input.GetAxis("Jump") > 0f)
        {
            isSelected = false;
            playableCharacter.Select();
            cameraControl.LookAt(player);
        }
    }
}