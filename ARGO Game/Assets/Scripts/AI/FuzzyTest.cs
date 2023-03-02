/*
 * Our fuzzy logic executer for our AI components
 * Worked on by: Jack Sinnott
 */

using UnityEngine;

public class FuzzyTest : MonoBehaviour
{
    // priorities
    float m_left;
    float m_stay;
    float m_right;

    // Animation curves used to visually represent our fuzzy sets data
    public AnimationCurve m_moveLeft;
    public AnimationCurve m_dontMove;
    public AnimationCurve m_moveRight;

    // Decays over time which means our bots have limited life cycles
    [SerializeField]
    float _differential = .5f;

    // reference so we can pass data to our HandleInput function
    BotHandler _aiUnit;

    // Bools used to control our HandleInput function
    bool _moveLeft = false, _moveRight = false;

    private void Awake()
    {
        _aiUnit = GetComponent<BotHandler>();
    }

    private void Update()
    {
        // clamp so we dont go negative with our anchor weight
        _differential = Mathf.Clamp(_differential, 0, .5f);
    }

    /// <summary>
    /// The meat and bones of our decision process! Using our animation curves we evaluate which set the value 
    /// passed in (t_inputValue) matches and that dictates our bots decisions
    /// </summary>
    /// <param name="t_inputValue">Our weight for movement(a value between 0 & 1)</param>
    public void Fuzzification(bool t_leftLaneRay, bool t_middleLaneRay, bool t_rightLaneRay)
    {

        float _laneWeight = calculateLeftRightMove(t_leftLaneRay, t_middleLaneRay, t_rightLaneRay);

        // We use the animationCurves evaluate function to map our input value to a decision set in our fuzzy logic
        m_left = m_moveLeft.Evaluate(_laneWeight);

        m_stay = m_dontMove.Evaluate(_laneWeight);

        m_right = m_moveRight.Evaluate(_laneWeight);

        //If we are in left lane we can only move right. We shouldnt move if our weight dictates we stay in lane
        if(_aiUnit._currentLane == BotHandler.Lane.LEFT_LANE && m_stay !> m_right)
        {
            _moveRight = true;
        }

        // if we are in right lane we can only move left. We shouldnt move if our weight dictates we stay in lane
        else if(_aiUnit._currentLane == BotHandler.Lane.RIGHT_LANE && m_stay! > m_left)
        {
            _moveLeft = true;
        }

        // if left set > other sets
        if(m_left > m_stay && m_left > m_right && _aiUnit._currentLane != BotHandler.Lane.LEFT_LANE)
        {
            _moveLeft = true;
        }

        // if right weight > other sets
        if(m_right > m_left && m_right > m_stay && _aiUnit._currentLane != BotHandler.Lane.RIGHT_LANE)
        {
            _moveRight = true;
        }

        // pass calcualted data to our input function for bot
        _aiUnit.HandleInput(_moveLeft, _moveRight);

        // reset the bools
        _moveLeft = false;
        _moveRight = false;

    }

    /// <summary>
    /// We have an anchor weight of .5 that gets added/subtracted to/from by our differential variable.
    /// Our differential gets subtracted from over time and by a random chance which will mean that bots get stupider over time.
    /// We then use our anchor weight to calculate if we should move and which direction to move
    /// </summary>
    /// <returns>our anchor weight for fuzzification function</returns>
    float calculateLeftRightMove(bool t_leftLaneRay, bool t_middleLaneRay, bool t_rightLaneRay)
    {
        float _initial_weight = .5f;

        int _percentileDifferential;

        _percentileDifferential = Random.Range(0, 100);

        UpdateDifferential(_percentileDifferential);

        int _switchLeftRight = Random.Range(0, 10);

        _switchLeftRight = Mathf.Clamp(_switchLeftRight, 0, 10);

        if(t_leftLaneRay)
        {
            _switchLeftRight = 0;
        }
        else if(t_middleLaneRay)
        {
            if(_aiUnit._currentLane == BotHandler.Lane.LEFT_LANE)
            {
                _switchLeftRight = 10;
            }
            else if(_aiUnit._currentLane == BotHandler.Lane.RIGHT_LANE)
            {
                _switchLeftRight = 0;
            }
        }
        else if(t_rightLaneRay)
        {
            _switchLeftRight = 10;
        }

        _initial_weight = MoveDirection(_switchLeftRight, _initial_weight);

        return _initial_weight;

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

    /// <summary>
    /// For each difficulty we have a different percentage chance to subtract .05f from our differential
    /// </summary>
    /// <param name="t_percentageReading">the value generated between 0 and 100</param>
    public void UpdateDifferential(int t_percentageReading)
    {

        // I hate this and instead want to change the fuzzy sets values to limit the size of the sets rather than just commit seppaku
        switch (_aiUnit._diff)
        {
            case BotHandler.Difficulty.EASY:
                if (t_percentageReading > 0 && t_percentageReading < 25)
                {
                    _differential -= .05f;
                }
                break;
            case BotHandler.Difficulty.MODERATE:
                if (t_percentageReading > 0 && t_percentageReading < 10)
                {
                    _differential -= .05f;
                }
                break;
            case BotHandler.Difficulty.HARD:
                if (t_percentageReading > 0 && t_percentageReading < 2)
                {
                    _differential -= .05f;
                }
                break;
            default:
                break;
        }
    }
}
