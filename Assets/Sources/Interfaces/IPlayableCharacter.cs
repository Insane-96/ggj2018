using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayableCharacter {
    /// <summary>
    /// Called to select the object and take control of it
    /// </summary>
	void Select();
}
