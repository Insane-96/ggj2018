using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDoor {

    /// <summary>
    /// Changes what the camera is looking at
    /// </summary>
    /// <param name="gameObject">The GameObject the camera will start looking at</param>
    void OpenDoor();
}
