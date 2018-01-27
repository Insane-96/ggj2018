using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogControl : MonoBehaviour
{
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();

    }

    void OnTriggerEnter(Collider collider)
    {
        IPlayableCharacter playableCharacter = collider.gameObject.GetComponent<IPlayableCharacter>();

        if (playableCharacter != null)
        {
            var main = ps.main;
            main.loop = false;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        var main = ps.main;
        main.loop = true;
        main.startLifetime = 1f;

        ps.Play();
    }
}
