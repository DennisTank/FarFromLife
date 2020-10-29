using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public float life;
    public Image Bar;
    public bool autoRegenerate;
    public float ReGenPerCent;

    [HideInInspector]public float currentLife;

    private float value;

    MotionCurve mc;
    void Start()
    {
        value = currentLife = life;
        mc = new MotionCurve();
        if (autoRegenerate) {
            InvokeRepeating("Regenerate", 0,5);
        }
    }
    void Regenerate() {
        currentLife += ((ReGenPerCent/100f) * life);
    }
    void Update()
    {
        currentLife = Mathf.Clamp(currentLife,0,life);

        if (value > currentLife)
        {
            value = mc.CurveLow(value, currentLife, 0.2f);
        }
        else if (value < currentLife) {
            value = mc.CurveHigh(value,currentLife,0.2f);
        }
        Bar.fillAmount = (value/life);
    }
}
