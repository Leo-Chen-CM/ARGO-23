using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField]
    Vector3 m_movement = new Vector3();

    [SerializeField]
    Rigidbody m_rigidbody;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }


    [Client]
    // Update is called once per frame
    void Update()
    {
        if (!isOwned) return;

        if (Input.GetKeyDown(KeyCode.Space)) { CmdMove(); }
    }

    [Command]
    private void CmdMove()
    {
        RpcMove();
    }

    [ClientRpc]
    private void RpcMove() => m_rigidbody.velocity = m_movement;
}
