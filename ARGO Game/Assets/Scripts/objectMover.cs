using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class objectMover : MonoBehaviour
{
    public float speed;

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.z -= speed;
        if(pos.z < -20)
        {
            Destroy(gameObject);
        }
        transform.position = pos;
    }
}
