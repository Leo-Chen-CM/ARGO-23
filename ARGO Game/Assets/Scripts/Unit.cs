/// <summary>
/// Our player script which outlines all the logic the player follows
/// Worked on by: Jack Sinnott
/// </summary>

using UnityEngine;

public class Unit : MonoBehaviour
{
    // For our ground raycast check
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _rayDistance = 5f;
    Vector3 _direction = Vector3.down;
    Ray _ray;

    public float _jumpForce = 5;

    public Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        // Update our ray info so we are constantly drawing from player current position
        _ray = new Ray(transform.position, transform.TransformDirection(_direction * _rayDistance));
        Debug.DrawRay(transform.position, transform.TransformDirection(_direction * _rayDistance));

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
            Debug.Log("Did Hit");
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(_direction * _rayDistance), Color.white);
            Debug.Log("Did not Hit");
            return false;
        }
    }

    public float GetJumpForce()
    {
        return _jumpForce;
    }
}
