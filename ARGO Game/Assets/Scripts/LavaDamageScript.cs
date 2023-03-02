using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamageScript : MonoBehaviour
{
    public GameObject gm;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hitting the lava on the floor");
            gm.gameObject.GetComponent<gameManager>().reduceHealth();

        }
    }
}
 
