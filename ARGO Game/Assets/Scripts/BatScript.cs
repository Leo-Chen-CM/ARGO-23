using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : Obstacle
{
    /// the speed at which the bat moves towards the player
    public float speed;
    /// reference to the gamemanager
    public GameObject gm;
    /// the offset off the ground the bat floats
    public Vector3 offset;

    private void Start()
    {
        transform.position += offset;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    /// <summary>
    /// This function moves the Bat towards the player and deletes it after it goes offscreen
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gm.gameObject.GetComponent<gameManager>().reduceHealth();
        }
    }
}
