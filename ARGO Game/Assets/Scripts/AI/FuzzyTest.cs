using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyTest : MonoBehaviour
{
    // priorities
    float m_left;
    float m_stay;
    float m_right;

    public AnimationCurve m_moveLeft;
    public AnimationCurve m_dontMove;
    public AnimationCurve m_moveRight;

    BotHandler _aiUnit;

    bool _moveLeft = false, _moveRight = false;

    private void Awake()
    {
        _aiUnit = GetComponent<BotHandler>();
    }


    public void Fuzzification(float t_inputValue)
    {
        m_left = m_moveLeft.Evaluate(t_inputValue);

        m_stay = m_dontMove.Evaluate(t_inputValue);

        m_right = m_moveRight.Evaluate(t_inputValue);

        if(_aiUnit._currentLane == BotHandler.Lane.LEFT_LANE)
        {
            _moveRight = true;
        }

        else if(_aiUnit._currentLane == BotHandler.Lane.RIGHT_LANE)
        {
            _moveLeft = true;
        }

        if(m_left > m_stay && m_left > m_right && _aiUnit._currentLane != BotHandler.Lane.LEFT_LANE)
        {
            _moveLeft = true;
        }

        if(m_right > m_left && m_right > m_stay && _aiUnit._currentLane != BotHandler.Lane.RIGHT_LANE)
        {
            _moveRight = true;
        }



        _aiUnit.HandleInput(_moveLeft, _moveRight);

        _moveLeft = false;
        _moveRight = false;

        Debug.Log("m_left: " + m_left + "\n m_stay: " + m_stay + "\nm_right: " + m_right);
    }
}
