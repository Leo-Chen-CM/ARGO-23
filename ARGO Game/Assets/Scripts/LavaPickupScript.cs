using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPickupScript : Obstacle
{
    public float speed;
    public GameObject lavaFloor;
    float LavaLiveTime = 10.0f;
    public GameObject gm;
    Vector3 floorVec;


    private void Start()
    {
        floorVec = transform.position;
        floorVec.y = floorVec.y - 1.2f;
        floorVec.z = floorVec.z - 38;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            GameObject newLavaFloor = Instantiate(lavaFloor, floorVec, Quaternion.identity);
            Destroy(newLavaFloor, LavaLiveTime);

        }
    }
}