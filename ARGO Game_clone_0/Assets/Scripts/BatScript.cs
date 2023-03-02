using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : Obstacle
{
    public float speed;
    public GameObject gm;
    public Vector3 offset;

    private void Start()
    {
        transform.position += offset;
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
        Debug.Log("interacted with obstacle");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gm.gameObject.GetComponent<gameManager>().reduceHealth();
        }
    }
}
