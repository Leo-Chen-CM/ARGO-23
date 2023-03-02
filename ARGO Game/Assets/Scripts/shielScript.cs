using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shielScript : Obstacle
{
    public float speed;
    public GameObject gm;
   

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
            gm.gameObject.GetComponent<gameManager>().increaseHp();
        }
    }
}
