using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class InputHandler : NetworkBehaviour
{
    public Unit _unit;
    private ICommand _bA, _bD, _bS, _bSpace; // Our buttons A, D, S, Space so we can link the button to any command at runtime
    public static List<ICommand> _oldCommands = new List<ICommand>(); // If we store commands in a list we can backtrack through commands


    private void Awake()
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
    }

    public void OnJump()
    {
        _bSpace.Execute(_unit, _bSpace);
        Debug.Log("You tried to jump");
    }

    public void OnTurn()
    {
        Debug.Log("You tried to move left/right");
    }
}