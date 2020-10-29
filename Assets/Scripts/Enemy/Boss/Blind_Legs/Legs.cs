using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    public Vector3 direction;
    public IK ik;
    public float maxdistance;
    public float speed;

    Vector3 point,currentPoint;
    bool now;

    void Start()
    {
        setTarget();
        ik.target = currentPoint = point;
        now = false;
    }
    
    void Update()
    {
        setTarget();
        if (Vector3.Distance(ik.target, point) > maxdistance) {
            now = true;
            currentPoint = point;
        }
        Move();
        
    }
    void Move() {
        if (Vector3.Distance(ik.target, currentPoint) > 0.01f && now)
        {
            ik.target = Vector3.MoveTowards(ik.target,currentPoint, speed * Time.deltaTime);
        }
        else {
            ik.target = currentPoint;
            now = false; 
        }
    }
    void setTarget() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(direction),out hit)) {
            point = hit.point;
        }
    }
}
