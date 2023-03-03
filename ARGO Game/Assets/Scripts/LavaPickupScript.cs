using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPickupScript : Obstacle
{
    /// the speed it moves towards player
    public float speed;
    /// the floor object that spawns on collision
    public GameObject lavaFloor;
    float LavaLiveTime=10.0f;
    /// reference to the game manager
    public GameObject gm;
    Vector3 floorVec;
   
    
    private void Start()
    {
        floorVec = transform.position;
        floorVec.y = floorVec.y - 1.2f;
        floorVec.z = floorVec.z - 38;
        speed = 1;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    /// <summary>
    /// This function moves the lavaPickup towards the player and deletes it after it goes offscreen
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
            AudioManager.Instance().PlaySoundEffect(AudioManager.SoundEffect.Injured);
            GameObject newLavaFloor = Instantiate(lavaFloor, floorVec, Quaternion.identity);
            Destroy(newLavaFloor, LavaLiveTime);
           
        }
    }
}
