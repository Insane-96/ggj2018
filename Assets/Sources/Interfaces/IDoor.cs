using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDoorOpen {

    /// <summary>
    /// Changes what the camera is looking at
    /// </summary>
    /// <param name="gameObject">The GameObject the camera will start looking at</param>
    bool isOpening { get; set; }
}
