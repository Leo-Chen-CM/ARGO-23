using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : Obstacle
{
    public float speed;
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
