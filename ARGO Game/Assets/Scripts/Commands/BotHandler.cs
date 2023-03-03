/*
 * Our bots main script. Handles all bot data and input mappings (from fuzzy logic script)
 * Worked on by: Jack Sinnott
 */

using log4net.Util;
using System.Collections.Generic;
using UnityEngine;

public class BotHandler : MonoBehaviour
{
    /// <summary>
    /// Enum to easily parse our bot difficulty when needed
    /// </summary>
    public enum Difficulty
    {
        EASY = 0,
        MODERATE = 1,
        HARD = 2
    }

    /// <summary>
    /// Reference to our current lane so we dont move left in left lane etc
    /// </summary>
    public enum Lane
    {
        LEFT_LANE = 3,
        MIDDLE_LANE = 4,
        RIGHT_LANE = 5
    }

    // Our fuzzy logic implementor
    FuzzyTest _ft;

    // Enum instantiation
    public Difficulty _diff;
    public Lane _currentLane;

    // Command pattern links
    public Unit _unit;
    private ICommand _moveLeft, _moveRight, _jump, _slide; // Our buttons A, D, S, Space so we can link the button to any command at runtime

    // 
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private LayerMask _laneMasks;
    [SerializeField] private float _unitRayDistance = 5f;
    [SerializeField] private float _laneRayDistance;
    Vector3 _direction = Vector3.forward;
    Ray _footRay;
    Ray _headRay;
    Ray _leftRay;
    Ray _middleRay;
    Ray _rightRay;

    bool _leftactive = false;
    bool _middleActive = false;
    bool _rightActive = false;

    bool head;
    bool foot;
    bool _leftRayTriggered;
    bool _middleRayTriggered;
    bool _rightRayTriggered;

    private void Awake()
    {
        //Bind keys to commands
        _moveLeft = new CommandMoveLeft();
        _moveRight = new CommandMoveRight();
        _slide = new CommandSlide();
        _jump = new CommandJump();

        _unit = GetComponent<Unit>();

        _diff = Difficulty.EASY;

        _currentLane = Lane.MIDDLE_LANE;

        _ft = GetComponent<FuzzyTest>();
    }

    private void Update()
    {
        
        // Update our lane so we know what movements are not allowed versus what is acceptable
        UpdateLane();

        // Sets up our rays from the player
        HandleRaycasting();

        // Meat and bones of decision handling
        CollisionHandler(_unitRayDistance);

    }

    /// <summary>
    /// Handled by our fuzzy logic which effects bools on botHandler side
    /// </summary>
    /// <param name="t_moveLeft">Should we move left? output: 0 or 1</param>
    /// <param name="t_moveRight">Should we move right? output: 0 or 1</param>
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

    }

    /// <summary>
    /// When ray triggers a collision pass info to our fuzzification function
    /// </summary>
    /// <param name="t_rayDist">the length of the ray</param>
    public void CollisionHandler(float t_rayDist)
    {
        head = Physics.Raycast(_headRay, t_rayDist, _obstacleMask);
        foot = Physics.Raycast(_footRay, t_rayDist, _obstacleMask);
        _leftRayTriggered = Physics.Raycast(_leftRay, t_rayDist, _laneMasks);
        _middleRayTriggered = Physics.Raycast(_middleRay, t_rayDist, _laneMasks);
        _rightRayTriggered = Physics.Raycast(_rightRay, t_rayDist, _laneMasks);

        if (head && foot)
        {
            _ft.Fuzzification(_leftRayTriggered, _middleRayTriggered, _rightRayTriggered);
        }
        else if (head)
        {
            // jump is possible
            Jump();
        }
        else if(foot)
        {
            // duck is posssible
            Slide();
        }
    }

    private void Jump()
    {
        _jump.Execute(_unit, _jump);
    }
    private void Slide()
    {
        _slide.Execute(_unit, _slide);
    }

    private void UpdateLane()
    {
        // update our lane info
        if (transform.position.x < -1f) _currentLane = Lane.LEFT_LANE;
        else if (transform.position.x > -1f && transform.position.x < 1f) _currentLane = Lane.MIDDLE_LANE;
        else if (transform.position.x > 1f) _currentLane = Lane.RIGHT_LANE;
    }

    private void HandleRaycasting()
    {
        // draw and update ray positions
        Vector3 pos = transform.position;
        pos.y *= 2;
        _headRay = new Ray(pos, transform.TransformDirection(_direction * _unitRayDistance));

        Debug.DrawRay(pos, transform.TransformDirection(_direction * _unitRayDistance));

        pos.y = 0;
        _footRay = new Ray(pos, transform.TransformDirection(_direction * _unitRayDistance));

        Debug.DrawRay(pos, transform.TransformDirection(_direction * _unitRayDistance));

        // effects our ray distance based on bot difficulty
        switch (_diff)
        {
            case Difficulty.EASY:
                _laneRayDistance = 5f;
                break;
            case Difficulty.MODERATE:
                _laneRayDistance = 7.5f;
                break;
            case Difficulty.HARD:
                _laneRayDistance = 10f;
                break;
            default:
                break;
        }

        // Sets bools to control lane rays
        switch (_currentLane)
        {
            case Lane.LEFT_LANE:
                _leftactive = false;
                _middleActive = true;
                _rightActive = true;
                break;
            case Lane.MIDDLE_LANE:
                _leftactive = true;
                _middleActive = false;
                _rightActive = true;
                break;
            case Lane.RIGHT_LANE:
                _leftactive = true;
                _middleActive = true;
                _rightActive = false;
                break;
            default:
                break;
        }


        if (_leftactive)
        {
            _leftRay = new Ray(new Vector3(-3.5f, 0, 0), transform.TransformDirection(_direction * _laneRayDistance));
            Debug.DrawRay(new Vector3(-3.5f, 0, 0), transform.TransformDirection(_direction * _laneRayDistance));
        }

        if (_middleActive)
        {
            _middleRay = new Ray(new Vector3(0, 0, 0), transform.TransformDirection(_direction * _laneRayDistance));
            Debug.DrawRay(new Vector3(0, 0, 0), transform.TransformDirection(_direction * _laneRayDistance));
        }
        if (_rightActive)
        {
            _rightRay = new Ray(new Vector3(3.5f, 0, 0), transform.TransformDirection(_direction * _laneRayDistance));
            Debug.DrawRay(new Vector3(3.5f, 0, 0), transform.TransformDirection(_direction * _laneRayDistance));
        }
    }
}