using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamageScript : MonoBehaviour
{
    /// reference to the game manager
    public GameObject gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gm.gameObject.GetComponent<gameManager>().reduceHealth();
           
        }
    }
}
