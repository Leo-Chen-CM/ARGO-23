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

    // external dependicies
    public FuzzyTriangle m_fuzz;
    public FuzzyOperators m_operators;

    public BotHandler _unit;


    private void Awake()
    {
        // external dependencies
        m_fuzz = GetComponent<FuzzyTriangle>();
        m_operators = GetComponent<FuzzyOperators>();
        _unit = GetComponent<BotHandler>();
    }


    public void Fuzzification(float t_inputValue)
    {
        m_left = m_moveLeft.Evaluate(t_inputValue);

        m_stay = m_dontMove.Evaluate(t_inputValue);

        m_right = m_moveRight.Evaluate(t_inputValue);

        Debug.Log("m_left: " + m_left + "\n m_stay: " + m_stay + "\nm_right: " + m_right);
    }
}
