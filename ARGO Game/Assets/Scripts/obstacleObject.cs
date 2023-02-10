using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleObject : Obstacle
{

    public float speed;

    private void FixedUpdate()
    {
        Movement();
    }

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
    public override void Interaction()
    {
        Debug.Log("interacted with obstacle");
    }

}
