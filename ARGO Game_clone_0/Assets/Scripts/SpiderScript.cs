using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : Obstacle
{

    public float speed;
    public GameObject SpidersWeb;
    public GameObject spawner;
    public GameObject gm;  
    Vector3 webPosition;
  
   public  int waittime = 4;
  
    private void Start()
    {
        webPosition = new Vector3(0f, 0.797f, -5.878f);
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
        // spawn web
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
           
            GameObject newWeb = Instantiate(SpidersWeb, webPosition, Quaternion.identity);
            Destroy(newWeb,waittime);
            gm.gameObject.GetComponent<gameManager>().reduceHealth();
         
          
        }
    }

   
}
