using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface to apply to player, check if able to open the door
/// </summary>
public interface IMainCharacter
{
    /// <summary>
    /// Check if main player have the key
    /// </summary>
    bool IHaveTheKey { get; set; }
}
