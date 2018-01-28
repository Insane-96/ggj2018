using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IEnemy
{

    private CharacterController character;

    private bool mouseDetected;

    private Vector3 mousePosition;

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
    public float frequencyIdle;

    private float tNoise;
    private float frequencyNoise;

    public Transform[] patrolSpot;

    public List<GameObject> patrols;

    private Vector3 direction;

    public Vector3 target;

    private int currentPatrolSpot;
    private float speed;

    private Transform currentNoisePos;

    private Animator animator;

    public bool rewind;

    private float tRay;
    private float frequencyRay;

    void Start()
    {
        currentPatrolState = PatrolState.Forward;

        tIdle = frequencyIdle;

        currentState = EnemyState.Idle;

        currentPatrolSpot = -1;

        frequencyNoise = 5f;

        speed = 0.014f;

        animator = this.GetComponent<Animator>();

        frequencyRay = 0.5f;

        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        EnemyRaycastDetection();

        tRay -= Time.deltaTime;

        if(tRay <= 0)
        {
            tRay = frequencyRay;
            Vector3 frw = this.transform.forward;

            RaycastHit hit;

            Ray ray = new Ray(transform.position, frw);

            if(Physics.Raycast(ray, out hit, 10f))
            {
                if(hit.transform.GetComponent<IMainCharacter>()!= null)
                {
                    GameOver();
                }
            }
        }

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
        if(patrols.Count <= 0)
        {
            patrols.Add(patrolSpot[0].gameObject);
            patrols.Add(patrolSpot[1].gameObject);
            patrols.Add(patrolSpot[2].gameObject);
            patrols.Add(patrolSpot[3].gameObject);
        }
        animator.SetBool("Movement", true);

       //Debug.Log("Enter Patrol State");

        patrols.Sort((a, b) =>
        {
            float distanceA = Vector3.Distance(transform.position, a.transform.position);
            float distanceB = Vector3.Distance(transform.position, b.transform.position);
            return distanceB.CompareTo(distanceA); 
        });

        foreach(GameObject patrol in patrols)
        {
            target = patrol.transform.position;
            direction = target - this.transform.position;
            transform.LookAt(target);
            direction.y = 0;

            if (Vector3.Distance(transform.position, patrol.transform.position) < 0.5f)
            {         
                currentState = EnemyState.Idle;
                patrols.Remove(patrol);
                return;
            }                     
        }

        character.Move(direction.normalized * speed);
    }

    //idle in the current spot
    private void Idle()
    {
        animator.SetBool("Movement", false);
        
       // Debug.Log("Enter Idle State");

        tIdle -= Time.deltaTime;

        if (tIdle <= 0)
        {
            tIdle = frequencyIdle;

            if (currentPatrolState == PatrolState.Forward)
            {
                currentPatrolSpot++;

                if (currentPatrolSpot >= patrolSpot.Length)
                {
                    if(!rewind)
                    {
                        currentPatrolSpot = 0;

                        Vector3 dir = patrolSpot[currentPatrolSpot].position - this.transform.position;

                        dir.y = 0;

                        transform.LookAt(dir);

                        currentState = EnemyState.Patrol;

                        return;
                    }
                    currentPatrolSpot--;
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

        Vector3 direction = currentNoisePos.position - this.transform.position;
        character.transform.LookAt(currentNoisePos.position);
      
        direction.y = 0;

        transform.position += direction.normalized * speed;

        Vector3 myPos = new Vector3((int)this.transform.position.x, (int)this.transform.position.y, (int)this.transform.position.z);

        Vector3 noisePos = new Vector3((int)currentNoisePos.position.x, (int)currentNoisePos.position.y, (int)currentNoisePos.position.z);

        Debug.Log("my pos " + myPos + "Noise pos" + noisePos);

        if (Vector3.Distance(transform.position, currentNoisePos.position) >= 2)
        {
            Debug.Log("il topo è scappato!");
            currentState = EnemyState.Idle;
        }

        if (myPos.x.Equals(noisePos.x) && myPos.z.Equals(noisePos.z))
        {
            Debug.Log("Near Noise");
            //wait time
            tNoise = frequencyNoise;
            currentState = EnemyState.NoiseIdle;
        }
    }

    private void EnemyRaycastDetection()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + new Vector3(0,0.5f,0), transform.forward * 1f, Color.red);
        bool OnFrontplayer = Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward,out hit, 1, 1 << LayerMask.NameToLayer("Player"));
        if (OnFrontplayer) GameOver();  
    }

    public void NoiseDetection(Transform NoisePosition)
    {
        mouseDetected = true;
        mousePosition = NoisePosition.position;

        Debug.Log("noise detected");
        currentNoisePos = NoisePosition;

        //Vector3 direction = currentNoisePos.position - this.transform.position;

       // direction.y = 0;
        
       // transform.LookAt(currentNoisePos);

        currentState = EnemyState.NoiseDetected;
    }


    private void GameOver()
    {
        Debug.Log("GAME OVER");    
    }


    //player spotted 
    private void OnTriggerEnter(Collider other)
    {
        //game over scene
    }
}
