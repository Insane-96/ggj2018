using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour, IPlayableCharacter, IMainCharacter

{
    public float speed;

    private Rigidbody body;

    private bool iHaveTheKey;
    private bool imNearTheDoor;

    public bool IHaveTheKey
    {
        get
        {
            return iHaveTheKey;
        }
        set
        {
            iHaveTheKey = value;
        }
    }

    public bool ImNearTheDoor
    {
        get
        {
            return imNearTheDoor;
        }
        set
        {
            imNearTheDoor = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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

        body.velocity = direction * speed;

        OpenDoor();

    }
   
          
    void OpenDoor()
    {
        RaycastHit hit;
        //Debug.DrawRay(transform.position, transform.forward * 1, Color.red);
        if (Input.GetAxis("Fire3") > 0 && Physics.Raycast(transform.position, transform.forward, out hit,1, 1 << LayerMask.NameToLayer("Door")))
        {       
            IDoorOpen door = hit.transform.GetComponent<IDoorOpen>();
            if (door != null)
                door.isOpening = true;
        }
    }

    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            i++;
            hitColliders[i].SendMessage("" + i);
        }
    }

    public void Select()
    {
        throw new System.NotImplementedException();
    }
}
