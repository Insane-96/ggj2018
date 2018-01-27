using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour, IPlayableCharacter, IMainCharacter
{
    public float speed;

    private Rigidbody body;

    private bool isSelected = true;

    private int current = 0;

    private bool checkOS = false;

    private Collider[] hitColliders;

    private bool iHaveTheKey;
    private bool imNearTheDoor;

    private ICameraControl cameraControl;

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
        cameraControl = Camera.main.GetComponent<ICameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = horizontal * Camera.main.transform.right + vertical * Camera.main.transform.forward;
            direction.y = 0;

            body.velocity = direction * speed;

            if (Input.GetMouseButtonDown(0))
            {
                hitColliders = ShpereFocus(body.position, 3f, 1 << 8);
                if (hitColliders.Length != 0)
                {
                    isSelected = false;
                    hitColliders[current].GetComponent<IFocusable>().Focus();
                    checkOS = true;
                    body.isKinematic = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && checkOS)
        {
            if (current + 1 < hitColliders.Length)
            {
                hitColliders[current].GetComponent<IFocusable>().Unfocus();
                current++;
                hitColliders[current].GetComponent<IFocusable>().Focus();
            }
            else
            {
                hitColliders[current].GetComponent<IFocusable>().Unfocus();
                current = 0;
                hitColliders[current].GetComponent<IFocusable>().Focus();
            }
        }

        OpenDoor();

        if (Input.GetKeyDown(KeyCode.E))
        {
            hitColliders[current].gameObject.GetComponent<IPlayableCharacter>().Select();
            cameraControl.LookAt(hitColliders[current].gameObject);
            hitColliders[current].GetComponent<IFocusable>().Unfocus();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            body.isKinematic = false;
            isSelected = true;
            hitColliders[current].GetComponent<IFocusable>().Unfocus();
        }
    }

    //Input.GetAxis("Fire3") >
    void OpenDoor()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 1, Color.red);
        if (Input.GetKey(KeyCode.P) && Physics.Raycast(transform.position, transform.forward, out hit,1, 1 << LayerMask.NameToLayer("Door")))
        {
            IDoor door = hit.transform.GetComponent<IDoor>();
            if (door != null)
            {
                Debug.Log("Door opened!");
                door.OpenDoor();
            }
                
        }
    }

    Collider[] ShpereFocus(Vector3 center, float radius, int layerMask)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, layerMask);
        return hitColliders;
    }

    public void Select()
    {
        isSelected = true;
        body.isKinematic = false;

    }
}
