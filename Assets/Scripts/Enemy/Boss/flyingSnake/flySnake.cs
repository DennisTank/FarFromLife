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
        dist = new int[4] { 30, 30, 30, 30 };
        y = x = 0;
        z = 1;
        yC = 0;
        InvokeRepeating("rotToPlayer",0,5);
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
            transform.position += Vector3.up * yC * 0.1f;

        }
        move();     
    }
    void rotToPlayer()
    {
        float x, z, angle;
        x = player.transform.position.x - transform.position.x;
        z = player.transform.position.z - transform.position.z;
        angle = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
        //inst bulls
    }
}
