using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    
	void Start () {
		
	}
	
	void Update () {
        transform.position += new Vector3(0.1f, 0, 0);
	}
}
