using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class MotionCurve 
{
    public float CurveHigh(float currentValue,float maxValue, float steps) {
        if (currentValue > maxValue) return maxValue;
        return (currentValue + steps);
    }
    public float CurveLow(float currentValue,float minValue, float steps) {
        if (currentValue < minValue) return minValue;
        return (currentValue - steps);
    } 
    public float CurveZero(float currentValue, float steps) {
        if (currentValue > 0.1f) return (currentValue - steps);
        else if (currentValue < -0.1f) return (currentValue + steps);
        else { return 0; }
    }
 
}
