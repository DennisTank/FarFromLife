using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionCurve 
{
    public float SpeedCurveHigh(float currentValue,float maxValue, float CurveOffset) {
        if (currentValue > maxValue) return maxValue;
        return (currentValue + CurveOffset);
    }
    public float SpeedCurveLow(float currentValue,float minValue, float CurveOffset) {
        if (currentValue < minValue) return minValue;
        return (currentValue - CurveOffset);
    } 
    public float SpeedCurveZero(float currentValue, float CurveOffset) {
        if (currentValue > 0.1f) return (currentValue - CurveOffset);
        else if (currentValue < -0.1f) return (currentValue + CurveOffset);
        else { return 0; }
    }
}
