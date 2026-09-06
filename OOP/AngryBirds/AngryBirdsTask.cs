using System;

namespace AngryBirds;

public static class AngryBirdsTask
{
    public static double FindSightAngle(double v, double distance)
    {
        if(v <= 0)
            return double.NaN;
        return Math.Asin(distance / (v * v) * 9.8)/2;
    }
}