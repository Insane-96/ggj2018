using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour, IPlayableCharacter
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
        isSelected = true;
    }

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (!isSelected)
            return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mouseDirection = (hit.point - transform.position).normalized;
            mouseDirection.y = 0;
            body.rotation = Quaternion.LookRotation(mouseDirection);
        }


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = horizontal * Camera.main.transform.right + vertical * Camera.main.transform.forward;
        direction.y = 0;

        body.velocity = direction * movementSpeed;
    }

    private void ReturnToPlayer()
    {
        isSelected = false;
        playableCharacter.Select();
        cameraControl.LookAt(player);
    }
}
