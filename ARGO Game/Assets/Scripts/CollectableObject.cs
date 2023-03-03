using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : Obstacle
{
    /// the speed at which it moves towards player
    public float speed;

    private void FixedUpdate()
    {
        speed = 1;
        Movement();
    }

    /// <summary>
    /// This function moves the Coin towards the player and deletes it after it goes offscreen
    /// </summary>
    public override void Movement()
    {
        Vector3 pos = transform.position;
        pos.z -= speed;
        if (pos.z < -20)
        {
            Destroy(gameObject);
        }
        transform.position = pos;
    }
}
