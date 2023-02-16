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

    public enum Lane
    {
        LEFT_LANE = 3,
        MIDDLE_LANE = 4,
        RIGHT_LANE = 5
    }


    FuzzyTest _ft;

    public float _test;

    Difficulty _diff;
    public Lane _currentLane;

    bool _calculated = false;

    public Unit _unit;
    private ICommand _moveLeft, _moveRight, _jump, _slide; // Our buttons A, D, S, Space so we can link the button to any command at runtime
    public static List<ICommand> _oldCommands = new List<ICommand>(); // If we store commands in a list we can backtrack through commands

    [SerializeField]
    float _differential = .5f;

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

        _currentLane = Lane.MIDDLE_LANE;

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

        if (transform.position.x < -1f) _currentLane = Lane.LEFT_LANE;
        else if (transform.position.x > -1f && transform.position.x < 1f) _currentLane = Lane.MIDDLE_LANE;
        else if (transform.position.x > 1f) _currentLane = Lane.RIGHT_LANE;

        Debug.Log("Current Lane reading: " + _currentLane.ToString());

        _ray = new Ray(transform.position, transform.TransformDirection(_direction * _rayDistance));
        Debug.DrawRay(transform.position, transform.TransformDirection(_direction * _rayDistance));

        CollisionHandler(_rayDistance);

    }

    public void HandleInput(bool t_moveLeft, bool t_moveRight)
    {

        if (t_moveLeft)
        {
            _moveLeft.Execute(_unit, _moveLeft);
        }

        else if (t_moveRight)
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
            _ft.Fuzzification(calculateMove());
        }
    }

    float calculateMove()
    {
        float _initial_weight = .5f;

        int _percentileDifferential;
        
        if (!_calculated)
        {

            _percentileDifferential = Random.Range(0, 100);

            Debug.Log("Percentage output: " + _percentileDifferential);
            switch (_diff)
            {
                case Difficulty.EASY:
                    if (_percentileDifferential > 0 && _percentileDifferential < 75)
                    {
                        _differential -= .05f;
                    }
                    break;
                case Difficulty.MODERATE:
                    if (_percentileDifferential > 0 && _percentileDifferential < 50)
                    {
                        _differential -= .05f;
                    }
                    break;
                case Difficulty.HARD:
                    if (_percentileDifferential > 0 && _percentileDifferential < 10)
                    {
                        _differential -= .05f;
                    }
                    break;
                default:
                    break;
            }

            int _switchLeftRight = Random.Range(0, 1);
            switch (_switchLeftRight)
            {
                case 0:
                    _initial_weight -= _differential;
                    break;
                case 1:
                    _initial_weight += _differential;
                    break;
                default:
                    break;
            }
        }
        return _initial_weight;

    }

}
