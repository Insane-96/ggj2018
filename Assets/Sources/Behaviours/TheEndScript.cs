using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEndScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.anyKey)
        {
            Debug.Log("LOAD SCENE");
            SceneManager.LoadScene("StarScene");
        }
	}
}
