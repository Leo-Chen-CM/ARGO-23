using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shielScript : Obstacle
{
    public float speed;
    public GameObject ShieldField;
    public Transform PLayerTransform;
    float activeTime = 5.0f;
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
            Vector3 offset = new Vector3( 0.0f,0.55f,0.0f);
            GameObject newShield = Instantiate(ShieldField, PLayerTransform.position- offset, Quaternion.identity);
            if(newShield.gameObject.activeInHierarchy==true)
            {
                gm.gameObject.GetComponent<gameManager>().isShieldActive= true;
            }
            else if(newShield.gameObject.activeInHierarchy==false)
            {
                gm.gameObject.GetComponent<gameManager>().isShieldActive = false;
            }
            Destroy(newShield, activeTime);
        }
    }
}
