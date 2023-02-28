using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : Obstacle
{
    public float speed;

    private void FixedUpdate()
    {
        speed = 1;
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
        Debug.Log("interacted with collectable");
    }
}
