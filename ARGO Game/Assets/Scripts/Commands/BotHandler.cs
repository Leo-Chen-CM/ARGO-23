using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHandler : MonoBehaviour
{
    public Unit _unit;
    private ICommand _moveLeft, _moveRight, _jump, _slide; // Our buttons A, D, S, Space so we can link the button to any command at runtime
    public static List<ICommand> _oldCommands = new List<ICommand>(); // If we store commands in a list we can backtrack through commands
    // For our ground raycast check
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _rayDistance = 5f;
    Vector3 _direction = Vector3.forward;
    Ray _ray;

    int _temp;
    private void Start()
    {
        //Bind keys to commands
        _moveLeft = new CommandMoveLeft();
        _moveRight = new CommandMoveRight();
        _slide = new CommandSlide();
        _jump = new CommandJump();

        _unit = GetComponent<Unit>();

    }

    private void Update()
    {
        // Update our ray info so we are constantly drawing from player current position
        _ray = new Ray(transform.position, transform.TransformDirection(_direction * _rayDistance));
        Debug.DrawRay(transform.position, transform.TransformDirection(_direction * _rayDistance));
        GenerateRandomNumber(); 
        HandleInput(_temp);
    }

    public void HandleInput(int t_temp)
    {
        
        if (Physics.Raycast(_ray, out RaycastHit _hit, _rayDistance, _obstacleMask) && t_temp == 0)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(_direction * _rayDistance), Color.yellow);
            Debug.Log("Did Hit");
            _moveLeft.Execute(_unit, _moveLeft);
        }

        if (Physics.Raycast(_ray, out RaycastHit _hit2, _rayDistance, _obstacleMask) && t_temp == 1)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(_direction * _rayDistance), Color.yellow);
            Debug.Log("Did Hit");
            _moveRight.Execute(_unit, _moveRight);
        }

        Debug.Log("Random value is:" + _temp + "\nOur current passed value is: " + t_temp);


        // _slide.Execute(_unit, _slide);


        //_jump.Execute(_unit, _jump);

    }

    void GenerateRandomNumber()
    {
        _temp = Random.Range(0, 2);
    }
}
