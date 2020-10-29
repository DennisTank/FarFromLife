using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject hexWheel, mainWheel;
    public GameObject WRsparks,smoke;
    public float speed, turningSpeed,rotateSpeed;
    public LayerMask Wall;

    [HideInInspector] public bool wallRunning,boosting;

    private float Curve, boost, wallRunCurve,dTime;
    private int direction;
    private bool leftWall, rightWall;

    MotionCurve mc;
    Animator anime;
    Rigidbody rg;
    TrailRenderer tr;

    private void Awake()
    {
        anime = GetComponent<Animator>();
        rg = GetComponent<Rigidbody>();
        tr = GetComponent<TrailRenderer>();
        tr.enabled = false;
        WRsparks.SetActive(false);
    }
    void Start()
    {
        Curve = direction = 0;
        wallRunCurve = 1;
        boost = 1;
        wallRunning =leftWall = rightWall = false;
        mc = new MotionCurve();
    }

    private void FixedUpdate()
    {
        AllRayCasting();
    }
    void Update()
    {
        dTime = Time.deltaTime;
        AllInputs();
    }
    void AllInputs() {
        if (!wallRunning )
        {
            if (Input.GetKey(KeyCode.LeftShift) && direction==1) { 
                boosting = true;
                WRsparks.transform.localPosition = new Vector3(0, -1, 0);
                if (boost < 2)
                {
                    Instantiate(smoke, transform.position + Vector3.down, transform.rotation);
                    WRsparks.SetActive(true);
                    tr.enabled = true;
                }
                else { 
                    WRsparks.SetActive(false);
                    tr.enabled = false;
                }

            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) && boosting){ 
                boosting = false;
                WRsparks.transform.localPosition = new Vector3(0, 0, 0);
                Instantiate(smoke, transform.position + Vector3.down, transform.rotation);
                WRsparks.SetActive(false);
                tr.enabled = false;
            }
            // moving Forward and Backward
            if (Input.GetKey(KeyCode.W) ) { moveForward(); }
            else if (Input.GetKey(KeyCode.S) ) { moveBackward(); }
            else
            {       
                direction = 0;
                Curve = mc.CurveZero(Curve, 0.03f);
            }

            // rotating left and right
            if (Input.GetKey(KeyCode.A) ) { rotateLeft(); }
            else if (Input.GetKey(KeyCode.D) ) { rotateRight(); }
            else
            {
                anime.SetBool("left", false);
                anime.SetBool("right", false);
            }
        }
        else 
        {
            if (leftWall)
            {
                anime.SetInteger("wallRun", 2);  
            }
            else if (rightWall)
            {
                anime.SetInteger("wallRun", 1);
            }

            Curve = mc.CurveHigh(Curve, 1, 0.03f);
            wallRunCurve = mc.CurveZero(wallRunCurve,0.005f);
            hexWheel.transform.Rotate(0, -1 * rotateSpeed, 0);
            mainWheel.transform.Rotate(0, -1 * rotateSpeed, 0);
            
        }

        if (Input.GetKeyDown(KeyCode.Space) && (leftWall||rightWall))
        {
            rg.velocity = Vector3.zero;
            rg.useGravity = false;
            wallRunning = true;
            WRsparks.SetActive(true);
            tr.enabled = true;
           
        }
        else if ((Input.GetKeyUp(KeyCode.Space) ||(!rightWall && !leftWall) )&& wallRunning) {
            rg.useGravity = true;
            rg.velocity = transform.TransformDirection(Vector3.forward) * 10;
            wallRunning = false;
            wallRunCurve = 1;
            anime.SetInteger("wallRun", 0);
            Instantiate(smoke,transform.position,transform.rotation);
            WRsparks.SetActive(false);
            tr.enabled = false;
        }

        // Translations
        if (wallRunning)
        {
            if (leftWall)
            {
                transform.Translate(-2 * dTime, wallRunCurve * speed * dTime, 2 * Curve * speed * dTime);
            }
            else if (rightWall)
            {
                transform.Translate(2 * dTime, wallRunCurve * speed * dTime, 2 * Curve * speed * dTime);
            }
            
        }
        else { transform.Translate(0, 0, boost * Curve * speed * dTime); }
        
    }
    void moveForward() {
        
        direction = 1;
        boost = (boosting) ? mc.CurveHigh(boost, 2, 0.01f) : mc.CurveLow(boost,1,0.1f); ;
        Curve = mc.CurveHigh(Curve, 1, 0.03f);
        hexWheel.transform.Rotate(0, -1 * boost *rotateSpeed, 0);
    }
    void moveBackward() {
        
        direction = -1;
        Curve = mc.CurveLow(Curve, -1, 0.03f);
        hexWheel.transform.Rotate(0, rotateSpeed, 0);
    }
    void rotateLeft() {
        anime.SetBool("left", true);
        anime.SetBool("right", false);
        transform.Rotate(0, -1 * turningSpeed * ((direction == -1) ? -1 : 1) , 0, Space.Self);
        mainWheel.transform.Rotate(0, -1 * rotateSpeed, 0);
    }
    void rotateRight() {
        anime.SetBool("left", false);
        anime.SetBool("right", true);
        transform.Rotate(0, turningSpeed * ((direction==-1)?-1:1), 0, Space.Self);
        mainWheel.transform.Rotate(0, -1 * rotateSpeed, 0);
    }
    void AllRayCasting() {
        rightWall=Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), 2, Wall);
        leftWall=Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), 2, Wall);
    }

}
