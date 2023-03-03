using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    /// <summary>
    /// This function moves the Obstacle towards the player and deletes it after it goes offscreen
    /// </summary>
    public abstract void Movement();
}
