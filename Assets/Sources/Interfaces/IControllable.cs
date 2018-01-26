using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface to apply to entities that can be transfered to by the player and then controlled 
/// </summary>
public interface IControllable {
    /// <summary>
    /// When the player has this object focused and can select it. 
    /// </summary>
    /// <returns>Returns true if the focus was succesfull</returns>
    bool Focus();

    /// <summary>
    /// De-selects the object when the player is in focus.
    /// </summary>
    /// <returns>Returns true if the unfocus was succesfull</returns>
    bool Unfocus();

    /// <summary>
    /// Called to select the object and take control of it
    /// </summary>
    void Select();

    /// <summary>
    /// Called to deselect the object and return to player control
    /// </summary>
    //void Deselect();
}
