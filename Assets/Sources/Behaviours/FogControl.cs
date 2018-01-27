using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogControl : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        IPlayableCharacter playableCharacter = collider.gameObject.GetComponent<IPlayableCharacter>();

        if (playableCharacter != null)
        {
            GetComponent<Renderer>().material.color = new Color(0, 1, 0);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        GetComponent<Renderer>().material.color = new Color(1, 0, 0);
    }
}
