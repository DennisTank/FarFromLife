using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flySnake : basicMovements
{
    private int[] dist;

    float yC;
    bool goHigh;
    MotionCurve mc = new MotionCurve();
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        checkMoves = false;
        goHigh = true;
        dist = new int[4] { 20, 20, 20, 20 };
        y = x = 0;
        z = 1;
        yC = 0;
        InvokeRepeating("randomRot",0,5);
    }
    private void FixedUpdate()
    {
        limitCheck(dist);
    }
    void Update()
    {
        if (forward)
        {
            speed = 0;
            if (right)
            {
                transform.eulerAngles += Vector3.down;
            }
            else {
                transform.eulerAngles += Vector3.up;
            }

        }
        else { 
            speed = 50;
            if (yC > 1 && goHigh) { goHigh = false; }
            else if (yC < -1 && !goHigh) { goHigh = true; }

            yC = (goHigh) ? mc.CurveHigh(yC,1,0.01f):mc.CurveLow(yC,-1,0.01f);
            
            transform.eulerAngles += Vector3.up*yC;
 
        }
        move();
    }
    void randomRot() {
        if (Random.Range(0, 5) > 3) {
            transform.eulerAngles += Vector3.up*Random.Range(-45,45);
        }
    }
}
