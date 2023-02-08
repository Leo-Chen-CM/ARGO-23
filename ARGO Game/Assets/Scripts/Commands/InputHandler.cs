using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Mirror;
public class InputHandler : NetworkBehaviour
{
    public Unit _unit;
    private ICommand _bA, _bD, _bS, _bSpace; // Our buttons A, D, S, Space so we can link the button to any command at runtime
    public static List<ICommand> _oldCommands = new List<ICommand>(); // If we store commands in a list we can backtrack through commands

    private void Start()
    {
        //Bind keys to commands
        _bA = new CommandMoveLeft();
        _bD = new CommandMoveRight();
        _bS = new CommandSlide();
        _bSpace = new CommandJump();

        _unit = GetComponent<Unit>();

    }

    [Client]
    private void Update()
    {
        if (!isOwned) return;
        HandleInput();
    }


    [Command]
    public void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            RpcMoveA();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            RpcMoveD();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            RpcMoveS();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RpcMoveSpace();
        }
    }

    [ClientRpc]
    private void RpcMoveA() => _bA.Execute(_unit, _bA);
    [ClientRpc]
    private void RpcMoveD() => _bD.Execute(_unit, _bD);
    [ClientRpc]
    private void RpcMoveS() => _bS.Execute(_unit, _bS);
    [ClientRpc]
    private void RpcMoveSpace() => _bSpace.Execute(_unit, _bSpace);
}
