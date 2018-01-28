using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    void Start()
    {

    }


    void Update()
    {
        if(Input.anyKey)
        {
            Debug.Log("LOAD SCENE");
            SceneManager.LoadScene("Game");
        }
    }
}
