using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class InputHandler : NetworkBehaviour
{
    public Unit _unit;
    private ICommand _bA, _bD, _bS, _bSpace; // Our buttons A, D, S, Space so we can link the button to any command at runtime
    public static List<ICommand> _oldCommands = new List<ICommand>(); // If we store commands in a list we can backtrack through commands

    // For mobile
    private Vector2 _startPos; // Start position of the screen swipe
    private int _pixelDistToDetect = 20;
    private bool _fingerDown;

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
        HandleInput();
    }

    public void HandleInput()
    {
        if (_fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == UnityEngine.TouchPhase.Began)
        {
            _startPos = Input.touches[0].position;
            _fingerDown = true;
        }

        if (/*Input.GetKeyDown(KeyCode.A) ||*/ _fingerDown && Input.touches[0].position.x <= _startPos.x - _pixelDistToDetect)
        {
            _fingerDown = false;
            Debug.Log("swipe Left");
            _bA.Execute(_unit, _bA);
        }

        if (/*Input.GetKeyDown(KeyCode.D) ||*/ _fingerDown && Input.touches[0].position.x >= _startPos.x + _pixelDistToDetect)
        {
            _fingerDown = false;
            Debug.Log("swipe Right");
            _bD.Execute(_unit, _bD);
        }

        if (/*Input.GetKeyDown(KeyCode.S) ||*/ _fingerDown && Input.touches[0].position.y <= _startPos.y - _pixelDistToDetect)
        {
            _fingerDown = false;
            Debug.Log("swipe down");
            _bS.Execute(_unit, _bS);
        }

        if (/*Input.GetKeyDown(KeyCode.Space) ||*/ _fingerDown && Input.touches[0].position.y >= _startPos.y + _pixelDistToDetect)
        {
            _fingerDown = false;
            Debug.Log("swipe up");
            _bSpace.Execute(_unit, _bSpace);
        }

        // TESTING ON PC

        if (_fingerDown == false && Input.GetMouseButtonDown(0))
        {
            _startPos = Input.mousePosition;
            _fingerDown = true;
        }
        
        if(_fingerDown)
        {
            if(Input.mousePosition.y >= _startPos.y + _pixelDistToDetect)
            {
                _fingerDown = false;
                Debug.Log("Swipe up");
                _bSpace.Execute(_unit, _bSpace);
            }

            else if (Input.mousePosition.y <= _startPos.y - _pixelDistToDetect)
            {
                _fingerDown = false;
                Debug.Log("Swipe down");
                _bS.Execute(_unit, _bSpace);
            }

            else if (Input.mousePosition.x >= _startPos.x + _pixelDistToDetect)
            {
                _fingerDown = false;
                Debug.Log("Swipe Right");
                _bD.Execute(_unit, _bSpace);
            }

            else if (Input.mousePosition.x <= _startPos.x - _pixelDistToDetect)
            {
                _fingerDown = false;
                Debug.Log("Swipe Left");
                _bA.Execute(_unit, _bSpace);
            }
        }

        if(_fingerDown && Input.GetMouseButtonUp(0))
        {
            _fingerDown = false;
        }

    }
}