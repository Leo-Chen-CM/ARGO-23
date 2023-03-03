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


    // reference so we can pass data to our HandleInput function
    BotHandler _aiUnit;

    // Bools used to control our HandleInput function
    bool _moveLeft = false, _moveRight = false;

    private void Awake()
    {
        _aiUnit = GetComponent<BotHandler>();
    }

    /// <summary>
    /// The meat and bones of our decision process! Using our animation curves we evaluate which set the value 
    /// passed in (t_inputValue) matches and that dictates our bots decisions
    /// </summary>
    /// <param name="t_inputValue">Our weight for movement(a value between 0 & 1)</param>
    public void Fuzzification(float t_inputValue)
    {
        // We use the animationCurves evaluate function to map our input value to a decision set in our fuzzy logic
        m_left = m_moveLeft.Evaluate(t_inputValue);

        m_stay = m_dontMove.Evaluate(t_inputValue);

        m_right = m_moveRight.Evaluate(t_inputValue);

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
}
