using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraControl
{
    /// <summary>
    /// Changes what the camera is looking at
    /// </summary>
    /// <param name="gameObject">The GameObject the camera will start looking at</param>
    void LookAt(GameObject gameObject);
}
