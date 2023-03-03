using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : Obstacle
{
    /// the speed the spider travels at
    public float speed;
    /// the spiderweb object to spawn
    public GameObject SpidersWeb;
    /// reference to the spawner
    public GameObject spawner;
    /// reference to the game manager
    public GameObject gm;  
    Vector3 webPosition;

    /// the delay at which the web gets destroyed
    public int waittime = 4;
  
    private void Start()
    {
        webPosition = new Vector3(0f, 0.797f, -5.878f);
    }
    private void FixedUpdate()
    {
        Movement();
    }

    /// <summary>
    /// This function moves the Spider towards the player and deletes it after it goes offscreen
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
            AudioManager.Instance().PlaySoundEffect(AudioManager.SoundEffect.Spider);
            GameObject newWeb = Instantiate(SpidersWeb, webPosition, Quaternion.identity);
            Destroy(newWeb,waittime);
            gm.gameObject.GetComponent<gameManager>().reduceHealth();
        }
    }

   
}
