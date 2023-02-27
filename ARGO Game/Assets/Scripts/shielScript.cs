using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shielScript : Obstacle
{
    public float speed;
    public GameObject ShieldField;
    public GameObject spawner;
    public Transform PLayerTransform;
    float activeTime = 5.0f;
   

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
            Vector3 offset = new Vector3( 0.0f,0.55f,0.0f);
            GameObject newWeb = Instantiate(ShieldField, PLayerTransform.position- offset, Quaternion.identity);
            Destroy(newWeb, activeTime);
        }
    }
}
