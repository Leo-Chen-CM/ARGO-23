using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleObject : Obstacle
{

    public float speed;
    public GameObject gm;
    public GameObject player;

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
        Debug.Log("interacted with obstacle");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gm.gameObject.GetComponent<gameManager>().reduceHealth();
            player.gameObject.GetComponent<Unit>().poisioned = true;
        }
    }

}
