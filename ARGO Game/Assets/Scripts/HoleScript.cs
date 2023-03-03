using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : Obstacle
{
    /// the speed at which it moves towards player
    public float speed;
    /// reference to the game manager
    public GameObject gm;

    private void Start()
    {
        Vector3 pos = transform.position;
        pos.y = -1.99f;
        transform.position = pos;
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    /// <summary>
    /// This function moves the Hole towards the player and deletes it after it goes offscreen
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
