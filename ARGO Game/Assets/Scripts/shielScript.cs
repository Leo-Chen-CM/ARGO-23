using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shielScript : Obstacle
{
    /// how fast the objects move
    public float speed;
    /// reference to the game manager
    public GameObject gm;

    private void FixedUpdate()
    {
        Movement();
        
    }

    /// <summary>
    /// This function moves the shield towards the player and deletes it after it goes offscreen
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
            gm.gameObject.GetComponent<gameManager>().increaseHp();
        }
    }
}
