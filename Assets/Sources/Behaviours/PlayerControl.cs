using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour, IPlayableCharacter 
{
	public float speed;

	private Rigidbody body;

	// Use this for initialization
	void Start()
	{
		body = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate()
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

		ExplosionDamage (body.position, 3f, 1 << 8);
	}

	void ExplosionDamage(Vector3 center, float radius, int layerMask)
	{
		Collider[] hitColliders = Physics.OverlapSphere(center, radius, layerMask);
		int i = 0;
		while (i < hitColliders.Length)
		{
			i++;
			hitColliders[i].SendMessage("" + i);
		}
	}

	public void Select()
	{
		
	}
}
