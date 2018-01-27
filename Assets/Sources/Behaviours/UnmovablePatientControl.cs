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
        isSelected = true;
    }

    void Start()
    {

    }

    void Update()
    {
        Scream(2f);
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
        isSelected = false;
        playableCharacter.Select();
        cameraControl.LookAt(player);
    }
}