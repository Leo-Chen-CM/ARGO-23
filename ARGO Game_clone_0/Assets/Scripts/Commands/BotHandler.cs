/*
 * Our bots main script. Handles all bot data and input mappings (from fuzzy logic script)
 * Worked on by: Jack Sinnott
 */

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
    Difficulty _diff;
    public Lane _currentLane;

    // Command pattern links
    public Unit _unit;
    private ICommand _moveLeft, _moveRight, _jump, _slide; // Our buttons A, D, S, Space so we can link the button to any command at runtime
    public static List<ICommand> _oldCommands = new List<ICommand>(); // If we store commands in a list we can backtrack through commands

    // Decays over time which means our bots have limited life cycles
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

        _diff = Difficulty.EASY;

        _currentLane = Lane.MIDDLE_LANE;

        _ft = GetComponent<FuzzyTest>();
    }

    private void Update()
    {
        // effects our ray distance based on bot difficulty
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

        // update our lane info
        if (transform.position.x < -1f) _currentLane = Lane.LEFT_LANE;
        else if (transform.position.x > -1f && transform.position.x < 1f) _currentLane = Lane.MIDDLE_LANE;
        else if (transform.position.x > 1f) _currentLane = Lane.RIGHT_LANE;

        // clamp so we dont go negative with our anchor weight
        _differential = Mathf.Clamp(_differential, 0, .5f);

        // draw and update ray position
        _ray = new Ray(transform.position, transform.TransformDirection(_direction * _rayDistance));
        Debug.DrawRay(transform.position, transform.TransformDirection(_direction * _rayDistance));

        // Meat and bones of decision handling
        CollisionHandler(_rayDistance);

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

        // _slide.Execute(_unit, _slide);


        //_jump.Execute(_unit, _jump);

    }

    /// <summary>
    /// When ray triggers a collision pass info to our fuzzification function
    /// </summary>
    /// <param name="t_rayDist">the length of the ray</param>
    public void CollisionHandler(float t_rayDist)
    {
        if (Physics.Raycast(_ray, out RaycastHit _hit, t_rayDist, _obstacleMask))
        {
            _ft.Fuzzification(calculateMove());
        }
    }

    /// <summary>
    /// We have an anchor weight of .5 that gets added/subtracted to/from by our differential variable.
    /// Our differential gets subtracted from over time and by a random chance which will mean that bots get stupider over time.
    /// We then use our anchor weight to calculate if we should move and which direction to move
    /// </summary>
    /// <returns>our anchor weight for fuzzification function</returns>
    float calculateMove()
    {
        float _initial_weight = .5f;

        int _percentileDifferential;

        _percentileDifferential = Random.Range(0, 100);

        UpdateDifferential(_percentileDifferential);

        int _switchLeftRight = Random.Range(0, 10);

        _initial_weight = MoveDirection(_switchLeftRight, _initial_weight);

        return _initial_weight;

    }

    /// <summary>
    /// For each difficulty we have a different percentage chance to subtract .05f from our differential
    /// </summary>
    /// <param name="t_percentageReading">the value generated between 0 and 100</param>
    void UpdateDifferential(int t_percentageReading)
    {
        switch (_diff)
        {
            case Difficulty.EASY:
                if (t_percentageReading > 0 && t_percentageReading < 75)
                {
                    _differential -= .05f;
                }
                break;
            case Difficulty.MODERATE:
                if (t_percentageReading > 0 && t_percentageReading < 50)
                {
                    _differential -= .05f;
                }
                break;
            case Difficulty.HARD:
                if (t_percentageReading > 0 && t_percentageReading < 10)
                {
                    _differential -= .05f;
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// When in the middle lane we generate a random number that dictates weighing for moving left/right or 
    /// if differential is low enough staying in our current lane
    /// </summary>
    /// <param name="t_switch">Our random number that effects left/right weighing</param>
    /// <param name="t_weight">A reference to our current weight</param>
    /// <returns></returns>
    float MoveDirection(int t_switch, float t_weight)
    {
        if (t_switch <= 5)
        {
            t_weight -= _differential;
        }
        else if (t_switch > 5 && t_switch <= 10)
        {
            t_weight += _differential;
        }

        return t_weight;
    }
}
