using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject hexWheel, MainWheel;
    public float speed, turningSpeed,rotateSpeed;

    private float Curve;

    MotionCurve mc;
    Animator anime;
    private void Awake()
    {
        anime = GetComponent<Animator>();
    }
    void Start()
    {
        Curve = 0;
        mc = new MotionCurve();
    }

   
    void Update()
    {
        AllInputs();
    }
    void AllInputs() {

        // moving Forward and Backward
        if (Input.GetKey(KeyCode.W))
        {
            Curve = mc.SpeedCurveHigh(Curve,1,0.005f);
            hexWheel.transform.Rotate(0,-1*rotateSpeed, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Curve = mc.SpeedCurveLow(Curve, -1, 0.005f);
            hexWheel.transform.Rotate(0,rotateSpeed, 0);
        }
        else 
        {
            Curve = mc.SpeedCurveZero(Curve,0.005f);
        }

        // rotating left and right
        if (Input.GetKey(KeyCode.A))
        {
            anime.SetBool("left", true);
            anime.SetBool("right", false);
            transform.Rotate(-1 * turningSpeed, 0, 0, Space.Self);
            MainWheel.transform.Rotate(0, -1 * rotateSpeed, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            anime.SetBool("left", false);
            anime.SetBool("right", true);
            transform.Rotate(turningSpeed, 0, 0, Space.Self);
            MainWheel.transform.Rotate(0, -1 * rotateSpeed, 0);
        }
        else {
            anime.SetBool("left", false);
            anime.SetBool("right", false);
        }

        transform.Translate(0,0,Curve*speed*Time.deltaTime);
    }
}
