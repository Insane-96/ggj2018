﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMainCharacter : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        IMainCharacter mainCharacter = collider.gameObject.GetComponent<IMainCharacter>();

        if (mainCharacter != null)
        {
            mainCharacter.IHaveTheKey = true;
            Destroy(this);
        }
    }
}
