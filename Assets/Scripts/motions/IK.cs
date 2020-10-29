using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK : MonoBehaviour
{
    public int numJoints;
    public int Iterations;
    public Vector3 target;
    public GameObject bendPoint;

    GameObject[] joints;
    Vector3[] Position;
    float[] jointDistance;
    
    float length, targetDistance;
    Vector3 targetDirection;

    void Start()
    {
        length = 0;
        joints = new GameObject[numJoints];
        Position = new Vector3[numJoints];
        jointDistance = new float[numJoints-1];

        Init();
    }
    void Update()
    {
        if (target == null) return;

        targetDistance = Vector3.Distance(Position[numJoints - 1], target);
        targetDirection = (target - Position[numJoints - 1]).normalized;

        setRot();
        getPos();
        PosCal();
        setPos();
    }
    void Init()
    {
        for (int i = 0; i < numJoints; i++) {
            joints[i] = (i == 0) ? gameObject : joints[i - 1].transform.parent.gameObject;
            Position[i] = joints[i].transform.position;
            if (i > 0) {
                jointDistance[i - 1] = Vector3.Distance(Position[i-1],Position[i]);
                length += jointDistance[i - 1];
            }
        }

    }
    void setRot() {
        for (int i = numJoints - 1; i > 0 ; i--) {
            joints[i].transform.rotation = Quaternion.LookRotation(joints[i - 1].transform.position - joints[i].transform.position);        
        }
    }
    void getPos() {
        for (int i = 0; i < numJoints; i++) {
            Position[i] = joints[i].transform.position;
        }
    }
    void PosCal() {
       
        if (targetDistance > length)
        {
            for (int i = numJoints - 2; i >= 0; i--)
            {
                Position[i] = Position[i + 1] + (targetDirection * jointDistance[i]);
            }
        }
        else {

            for (int iter = 0; iter < Iterations; iter++) {
                if (Vector3.Distance(Position[0], target) < 0.001f) { break; }

                for (int i = 0; i < numJoints - 1; i++) {
                    Position[i] = (i == 0) ? target :
                        Position[i - 1] + (bendPoint.transform.position - Position[0]).normalized * (jointDistance[i - 1]);
                }
                for (int i = numJoints - 2; i >= 0; i--) {
                    Position[i] = Position[i + 1] + (Position[i] - Position[i + 1]).normalized * (jointDistance[i]);
                }
            }
        }
    }
    void setPos() {
        for (int i = 0; i < numJoints; i++){
            joints[i].transform.position = Position[i];
        }
    }
}
