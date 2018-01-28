using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatControl : MonoBehaviour, IPlayableCharacter
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
		playableCharacter = player.GetComponent<IPlayableCharacter>();
        body = GetComponent<Rigidbody>();
        cameraControl = Camera.main.GetComponent<ICameraControl>();
    }

    void Update()
    {
        Movement();
        ReturnToPlayer();
        MakeNoise();
    }

    private void MakeNoise()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 6f);
        foreach (Collider col in cols)
        {
            IEnemy enemy = col.GetComponent<IEnemy>();
            if (enemy == null)
                continue;

            enemy.NoiseDetection(transform);
        }
    }

    private void Movement()
    {
        if (!isSelected)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = horizontal * Camera.main.transform.right + vertical * Camera.main.transform.forward;
        direction.y = 0;
        if (!Vector3.zero.Equals(direction))
            transform.rotation = Quaternion.LookRotation(-direction);

        body.velocity = direction * movementSpeed;
    }

    private void ReturnToPlayer()
    {
        if (!isSelected)
            return;

        if (Input.GetButtonDown("JB3"))
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            isSelected = false;
            playableCharacter.Select();
            cameraControl.LookAt(player);
        }
    }
}
