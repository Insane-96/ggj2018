using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IEnemy
{
    enum EnemyState
    {
        Idle,
        Patrol,
        NoiseDetected,
        NoiseIdle
    }

    enum PatrolState
    {
        Forward,
        Backward
    }

    private EnemyState currentState;

    private PatrolState currentPatrolState;

    private float tIdle;
    private float frequencyIdle;

    private float tNoise;
    private float frequencyNoise;

    public Transform[] patrolSpot;

    private int currentPatrolSpot;
    private float speed;

    private Transform currentNoisePos;

    private Animator animator;

    void Start()
    {
        currentPatrolState = PatrolState.Forward;

        frequencyIdle = 1f;

        tIdle = frequencyIdle;

        currentState = EnemyState.Idle;

        currentPatrolSpot = -1;

        frequencyNoise = 5f;

        speed = 0.02f;

        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                Idle();
                return;
            case EnemyState.Patrol:
                Patrol();
                return;
            case EnemyState.NoiseDetected:
                MoveToNoise();
                return;
            case EnemyState.NoiseIdle:
                NoiseIdle();
                return;

            default:
                break;
        }

    }

    //idle after noise detected
    private void NoiseIdle()
    {
        animator.SetBool("Movement", false);

        tNoise -= Time.deltaTime;

        if (tNoise <= 0)
        {
            currentState = EnemyState.Patrol;
        }
    }

    //patrol to the current patrol spot
    private void Patrol()
    {

        animator.SetBool("Movement", true);

        Debug.Log("Enter Patrol State");

        Vector3 direction = patrolSpot[currentPatrolSpot].position - this.transform.position;

        direction.y = 0;

        transform.position += direction.normalized * speed;

        Vector3 myPos = new Vector3((int)this.transform.position.x, (int)this.transform.position.y, (int)this.transform.position.z);

        Vector3 spotPos = new Vector3((int)patrolSpot[currentPatrolSpot].position.x, (int)patrolSpot[currentPatrolSpot].position.y, (int)patrolSpot[currentPatrolSpot].position.z);

        if (myPos.x.Equals(spotPos.x) && myPos.z.Equals(spotPos.z))
        {
            currentState = EnemyState.Idle;
        }

    }

    //idle in the current spot
    private void Idle()
    {
        animator.SetBool("Movement", false);

        Debug.Log("Enter Idle State");

        tIdle -= Time.deltaTime;

        if (tIdle <= 0)
        {
            tIdle = frequencyIdle;

            if (currentPatrolState == PatrolState.Forward)
            {
                currentPatrolSpot++;

                if (currentPatrolSpot >= patrolSpot.Length)
                {
                    currentPatrolSpot = 1;
                    currentPatrolState = PatrolState.Backward;
                }

                Vector3 direction = patrolSpot[currentPatrolSpot].position - this.transform.position;

                direction.y = 0;



                transform.LookAt(direction);

                currentState = EnemyState.Patrol;
                return;
            }
            else if (currentPatrolState == PatrolState.Backward)
            {
                currentPatrolSpot--;

                if (currentPatrolSpot <= 0)
                {
                    currentPatrolSpot = 0;
                    currentPatrolState = PatrolState.Forward;
                }

                Vector3 direction = patrolSpot[currentPatrolSpot].position - this.transform.position;

                direction.y = 0;

                transform.LookAt(direction);

                currentState = EnemyState.Patrol;
                return;

            }
        }
    }

    //when detect a noise, move to the noise spot
    private void MoveToNoise()
    {
        animator.SetBool("Movement", true);

        Debug.Log("Enter MoveToNoise State");

        Vector3 direction = currentNoisePos.position - this.transform.position;

        direction.y = 0;

        transform.position += direction.normalized * speed;

        Vector3 myPos = new Vector3((int)this.transform.position.x, (int)this.transform.position.y, (int)this.transform.position.z);

        Vector3 noisePos = new Vector3((int)currentNoisePos.position.x, (int)currentNoisePos.position.y, (int)currentNoisePos.position.z);

        Debug.Log("my pos " + myPos + "Noise pos" + noisePos);

        if (myPos.x.Equals(noisePos.x) && myPos.z.Equals(noisePos.z))
        {
            Debug.Log("Near Noise");
            //wait time
            tNoise = frequencyNoise;
            currentState = EnemyState.NoiseIdle;
        }
    }

    public void NoiseDetection(Transform NoisePosition)
    {
        Debug.Log("noise detected");
        currentNoisePos = NoisePosition;

        Vector3 direction = currentNoisePos.position - this.transform.position;

        direction.y = 0;

        transform.LookAt(direction);

        currentState = EnemyState.NoiseDetected;
    }

    //player spotted 
    private void OnTriggerEnter(Collider other)
    {
        //game over scene
    }
}
