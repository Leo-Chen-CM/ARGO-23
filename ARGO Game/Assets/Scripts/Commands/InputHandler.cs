using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Unit _unit;
    private ICommand _bA, _bD, _bS, _bSpace; // Our buttons A, D, S, Space so we can link the button to any command at runtime
    public static List<ICommand> _oldCommands = new List<ICommand>(); // If we store commands in a list we can backtrack through commands

    private void Start()
    {
        //Bind keys to commands
        _bA = new CommandMoveLeft();
        _bD = new CommandMoveRight();
        //_bS = new CommandSlide();
        _bSpace = new CommandJump();

        _unit = GetComponent<Unit>();

    }

    private void Update()
    {
        HandleInput();
    }

    public void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            _bA.Execute(_unit, _bA);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _bD.Execute(_unit, _bD);
        }
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    _bS.Execute();
        //}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _bSpace.Execute(_unit, _bSpace);
        }
    }
}
