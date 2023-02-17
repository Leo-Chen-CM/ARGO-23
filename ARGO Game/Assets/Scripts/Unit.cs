/// <summary>
/// Our player script which outlines all the logic the player follows
/// Worked on by: Jack Sinnott
/// </summary>

using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // For our ground raycast check
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _rayDistance = 5f;
    public bool poisioned = false;
    Vector3 _direction = Vector3.down;
    Ray _ray;
    Color _color;
    public float _jumpForce = 5;
    private float waitTime = 1.5f;
    public Rigidbody _rb;

  //  private gameManager gm;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _color = GetComponent<SpriteRenderer>().material.color;
    }

        private void Update()
    {
        // Update our ray info so we are constantly drawing from player current position
        _ray = new Ray(transform.position, transform.TransformDirection(_direction * _rayDistance));
        Debug.DrawRay(transform.position, transform.TransformDirection(_direction * _rayDistance));

        

    }
    private void FixedUpdate()
    {
        StartCoroutine(poisonedChecker());
    }
    /// <summary>
    /// Draws a raycast from the bottom of the player downwards to check for ground collisions
    /// </summary>
    /// <returns>True if ground layerMask collides with ray, false otherwise</returns>
    public bool IsGrounded()
    {
        if (Physics.Raycast(_ray, out RaycastHit _hit, _rayDistance, _groundMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(_direction * _rayDistance), Color.yellow);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(_direction * _rayDistance), Color.white);
            return false;
        }
    }

    public float GetJumpForce()
    {
        return _jumpForce;
    }

    // currently triggers for all players not just user - action required
    ////private void OnTriggerEnter(Collider other)
    ////{
    ////    if (other.tag == "Pickup")
    ////    {
    ////        gm.addScore();
    ////        Destroy(other.gameObject);
    ////    }
    ////    else if (other.tag == "Obstacle")
    ////    {
    ////        if (!gm.reduceHealth())
    ////        {
    ////            Debug.Log("player died");
    ////        }
    ////        Destroy(other.gameObject);
    ////    }
    ////}
    private IEnumerator poisonedChecker()
    {
      

        while (poisioned == true )
        {
           
           _color.g += 0.005f;
           gameObject.GetComponent<SpriteRenderer>().material.color = _color;
           
           yield return new WaitForSeconds(waitTime);
            _color.g = 0;
            poisioned = false;

        }
    }


}
