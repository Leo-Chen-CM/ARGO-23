using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyOperators : MonoBehaviour
{
    public float FuzzyAND(float t_a, float t_b)
    {
        return Mathf.Min(t_a, t_b);
    }

    public float FuzzyOR(float t_a, float t_b)
    {
        return Mathf.Max(t_a, t_b);
    }

    public float FuzzyNOT(float t_a)
    {
        return 1.0f - t_a;
    }
        
}
