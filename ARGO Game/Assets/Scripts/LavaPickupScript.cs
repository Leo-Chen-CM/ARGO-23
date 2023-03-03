using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPickupScript : Obstacle
{
    public float speed;
    public GameObject lavaFloor;   
    
    private void Start()
    {
        speed = 1;
    }

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

    }
}
