using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHandler : MonoBehaviour
{
    public enum Difficulty
    {
        EASY = 0,
        MODERATE = 1,
        HARD = 2
    }

    FuzzyTest _ft;

    public float _test;

    Difficulty _diff;
    public Unit _unit;
    private ICommand _moveLeft, _moveRight, _jump, _slide; // Our buttons A, D, S, Space so we can link the button to any command at runtime
    public static List<ICommand> _oldCommands = new List<ICommand>(); // If we store commands in a list we can backtrack through commands

    // For our ground raycast check
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _rayDistance = 2.5f;
    Vector3 _direction = Vector3.forward;
    Ray _ray;

    private void Awake()
    {
        //Bind keys to commands
        _moveLeft = new CommandMoveLeft();
        _moveRight = new CommandMoveRight();
        _slide = new CommandSlide();
        _jump = new CommandJump();

        _unit = GetComponent<Unit>();

        _diff = Difficulty.HARD;

        _ft = GetComponent<FuzzyTest>();
    }

    private void Update()
    {
        switch (_diff)
        {
            case Difficulty.EASY:
                _rayDistance = 2.5f;
                break;
            case Difficulty.MODERATE:
                _rayDistance = 5f;
                break;
            case Difficulty.HARD:
                _rayDistance = 7.5f;
                break;
            default:
                break;
        }

        _ray = new Ray(transform.position, transform.TransformDirection(_direction * _rayDistance));
        Debug.DrawRay(transform.position, transform.TransformDirection(_direction * _rayDistance));

        CollisionHandler(_rayDistance);

    }

    public void HandleInput(float t_temp)
    {

        if (t_temp <= .3)
        {
            _moveLeft.Execute(_unit, _moveLeft);
        }

        if (t_temp >= .7)
        {
            _moveRight.Execute(_unit, _moveRight);
        }


        // _slide.Execute(_unit, _slide);


        //_jump.Execute(_unit, _jump);

    }

    public void CollisionHandler(float t_rayDist)
    {
        if (Physics.Raycast(_ray, out RaycastHit _hit, t_rayDist, _obstacleMask))
        {
            _ft.Fuzzification(t_rayDist);
        }
    }

}