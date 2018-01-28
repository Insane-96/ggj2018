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
        Scream(6f);
        ReturnToPlayer();
    }

    private void Scream(float noiseRadius)
    {
        if (!isSelected)
            return;

        //TODO Scream
        Collider[] cols = Physics.OverlapSphere(transform.position, noiseRadius);
        foreach (Collider col in cols)
        {
            IEnemy enemy = col.GetComponent<IEnemy>();
            if (enemy == null)
                continue;

            enemy.NoiseDetection(transform);
        }
    }

    private void ReturnToPlayer()
    {
        if (!isSelected)
            return;

        if (Input.GetButtonDown("JB3") && Vector3.Distance(player.transform.position, this.transform.position) <= 6.0f)
        {
            isSelected = false;
            playableCharacter.Select();
            cameraControl.LookAt(player);
        }
    }
}