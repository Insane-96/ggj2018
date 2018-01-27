using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableObject : MonoBehaviour, IFocusable
{
    private Transform button;
    private Transform mainCamera;

    public void Focus()
    {
        button = transform.Find("Button");
        mainCamera = Camera.main.transform;

        button.gameObject.SetActive(true);
        button.transform.forward = mainCamera.forward * -1;
    }

    public void Unfocus()
    {
        button.gameObject.SetActive(false);
    }
}
