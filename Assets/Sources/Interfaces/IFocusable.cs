using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface to apply to entities that can be transfered to by the player and then controlled 
/// </summary>
public interface IFocusable
{
    /// <summary>
    /// When the player has this object focused and can select it. 
    /// </summary>
    void Focus();

    /// <summary>
    /// De-selects the object when the player is in focus.
    /// </summary>
    void Unfocus();

    /// <summary>
    /// Called to deselect the object and return to player control
    /// </summary>
    //void Deselect();
}
