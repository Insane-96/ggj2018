using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePatientControl : MonoBehaviour, IPlayableCharacter
{
    private bool isSelected = false;
    public ICameraControl cameraControl;
    public GameObject player;
    private IPlayableCharacter playableCharacter;
    private Rigidbody body;

    public float movementSpeed;

    public void Select()
    {
        playableCharacter = player.GetComponent<IPlayableCharacter>();
        cameraControl.LookAt(this.gameObject);
        isSelected = true;
        this.GetComponent<Rigidbody>().isKinematic = false;
    }

    void Start()
    {
        body = GetComponent<Rigidbody>();
        cameraControl = Camera.main.GetComponent<ICameraControl>();
    }

    void Update()
    {
        Movement();
        ReturnToPlayer();
    }

    private void Movement()
    {
        if (!isSelected)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = horizontal * Camera.main.transform.right + vertical * Camera.main.transform.forward;
        direction.y = 0;

        body.velocity = direction * movementSpeed;
    }

    private void ReturnToPlayer()
    {
        if (!isSelected)
            return;

        if (Input.GetButtonDown("JB3") && Vector3.Distance(player.transform.position, this.transform.position) <= 6.0f)
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            isSelected = false;
            playableCharacter.Select();
            cameraControl.LookAt(player);
        }
    }

}
