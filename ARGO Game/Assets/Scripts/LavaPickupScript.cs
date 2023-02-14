using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPickupScript : Obstacle
{
    public float speed;
    public GameObject spawner;
    float LavaLiveTime=5.0f;

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


            //GameObject newLavaFloor = Instantiate(SpidersWeb, transform.position, Quaternion.identity);
            //Destroy(newLavaFloor, LavaLiveTime);
            //int check = spawner.gameObject.GetComponent<Spawner>().SpiderCount = 0;
            //Debug.Log(check);
        }
    }
}
