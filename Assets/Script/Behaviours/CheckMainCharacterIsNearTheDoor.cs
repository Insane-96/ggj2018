using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMainCharacterIsNearTheDoor : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        IMainCharacter mainCharacter = collider.gameObject.GetComponent<IMainCharacter>();

        if (mainCharacter.IHaveTheKey)
        {
            mainCharacter.ImNearTheDoor = true;
            Debug.Log(string.Format("I have the key: {0}", mainCharacter.ImNearTheDoor));


            mainCharacter.IHaveTheKey = false;
            mainCharacter.ImNearTheDoor = false;
        }
    }
}
