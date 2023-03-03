using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class InputHandler : NetworkBehaviour
{
    public Unit _unit;
    private ICommand _bLeft, _bRight, _bSlide, _bSpace; // Our buttons A, D, S, Space so we can link the button to any command at runtime
    public static List<ICommand> _oldCommands = new List<ICommand>(); // If we store commands in a list we can backtrack through commands

    // For mobile
    private Vector2 _startPos; // Start position of the screen swipe
    private int _pixelDistToDetect = 30;
    private bool _fingerDown;
    public bool _offline;

    [SerializeField] private bool onPC = false;

    private void Awake()
    {
        //Bind keys to commands
        _bLeft = new CommandMoveLeft();
        _bRight = new CommandMoveRight();
        _bSlide = new CommandSlide();
        _bSpace = new CommandJump();

        _unit = GetComponent<Unit>();

    }


    [Client]
    private void Update()
    {
        if (!isOwned) return;
       HandleInput();
    }


    public void HandleInput()
    {
        if (!onPC)
        {
            if (_fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == UnityEngine.TouchPhase.Began)
            {
                _startPos = Input.touches[0].position;
                _fingerDown = true;
            }

            if (_fingerDown && Input.touches[0].position.x <= _startPos.x - _pixelDistToDetect)
            {
                _fingerDown = false;
                _bLeft.Execute(_unit, _bLeft);
            }

            if (_fingerDown && Input.touches[0].position.x >= _startPos.x + _pixelDistToDetect)
            {
                _fingerDown = false;
                _bRight.Execute(_unit, _bRight);
            }

            if (_fingerDown && Input.touches[0].position.y <= _startPos.y - _pixelDistToDetect)
            {
                _fingerDown = false;
                _bSlide.Execute(_unit, _bSlide);
            }

            if (_fingerDown && Input.touches[0].position.y >= _startPos.y + _pixelDistToDetect)
            {
                _fingerDown = false;
                _bSpace.Execute(_unit, _bSpace);
            }
        }
        // TESTING ON PC
        if (onPC)
        {
            if (_fingerDown == false && Input.GetMouseButtonDown(0))
            {
                _startPos = Input.mousePosition;
                _fingerDown = true;
            }

            if (_fingerDown)
            {
                if (Input.mousePosition.y >= _startPos.y + _pixelDistToDetect)
                {
                    _fingerDown = false;
                    _bSpace.Execute(_unit, _bSpace);
                }

                else if (Input.mousePosition.y <= _startPos.y - _pixelDistToDetect)
                {
                    _fingerDown = false;
                    _bSlide.Execute(_unit, _bSpace);
                }

                else if (Input.mousePosition.x >= _startPos.x + _pixelDistToDetect)
                {
                    _fingerDown = false;
                    _bRight.Execute(_unit, _bSpace);
                }

                else if (Input.mousePosition.x <= _startPos.x - _pixelDistToDetect)
                {
                    _fingerDown = false;
                    _bLeft.Execute(_unit, _bSpace);
                }
            }

            if (_fingerDown && Input.GetMouseButtonUp(0))
            {
                _fingerDown = false;
            }

        }

    }

}