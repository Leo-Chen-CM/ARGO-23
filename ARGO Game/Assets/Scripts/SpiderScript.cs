using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : Obstacle
{

    public float speed;
    public GameObject SpidersWeb;
    Vector3 webPosition;
    float opacityDecrement = 1;
   public  int waittime = 4;
    Color m_webOpactiy;
    private void Start()
    {
        webPosition = new Vector3(0f, 0.797f, -5.878f);
        m_webOpactiy = SpidersWeb.gameObject.GetComponent<Renderer>().material.color;
      
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

            Debug.Log("spider hit player");
            GameObject newWeb = Instantiate(SpidersWeb, webPosition, Quaternion.identity);
          
            Debug.Log(opacityDecrement);
            Destroy(newWeb,waittime);

        }
    }

   
}
