using UnityEngine;

public static class DeviationChecker
{
    public static Deviation GetDeviation(Vector2 limits, float distance)
    {
        if (limits.x <= distance && distance <= limits.y)
        {
            return Deviation.InLimits;
        }
        else if (limits.x < distance)
        {
            return Deviation.Less;
        }
        else
        {
            return Deviation.More;
        }
    }
}
