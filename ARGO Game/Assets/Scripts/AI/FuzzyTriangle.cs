using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyTriangle : MonoBehaviour
{
    /// <summary>
    /// Our triangle set for fuzzy logic
    /// </summary>
    /// <param name="t_inputValue">The value we use to compare in the triangle</param>
    /// <param name="t_x0">The bottom left of the triangle</param>
    /// <param name="t_x1">The peak of the triangle</param>
    /// <param name="t_x2">The bottom right of the triangle</param>
    /// <returns>The intersection point of the triangle</returns>
    public float FuzzyTriangleSet(float t_inputValue, float t_x0, float t_x1, float t_x2)
    {
        float result = 0;

        if ((t_inputValue <= t_x0) || (t_inputValue >= t_x2)) result = 0;
        else if (t_inputValue == t_x1) result = 1;
        else if ((t_inputValue > t_x0) && (t_inputValue < t_x1)) result = ((t_inputValue - t_x0) / (t_x1 - t_x0));
        else result = ((t_x2 - t_inputValue) / (t_x2 - t_x1));

        return result;
    }
}
