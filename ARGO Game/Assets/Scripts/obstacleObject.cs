using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleObject : Obstacle
{
    /// the speed at which it travels
    public float speed;
    /// reference to the game manager
    public GameObject gm;

    private void FixedUpdate()
    {
        Movement();
    }

    /// <summary>
    /// This function moves the rat towards the player and deletes it after it goes offscreen
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
            other.gameObject.GetComponent<Unit>().poisioned = true;
        }
    }

}
