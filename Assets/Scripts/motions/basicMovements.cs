using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicMovements: MonoBehaviour
{
    public LayerMask enemy;
    [HideInInspector]public bool left, right, forward, backward;

    protected bool checkMoves;
    protected float speed;
    protected int x, y, z;
    protected GameObject player;
    protected Vector3 target;
    private float dTime;
    
    protected void move() {
        dTime = Time.deltaTime;
        if (checkMoves) {
            if (left) { x = 1; }
            else if (right) { x = -1; }
            else if (left && right) { x = 0; }

            if (backward) { z = 1; }
            else if (forward) { z = -1; }
            else if (backward && forward) { z = 0; }
        }

        transform.Translate(x*speed*dTime, y * speed * dTime, z * speed * dTime);
    }
    protected void limitCheck(int[] distance) {
        left = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), distance[0],~enemy);
        right = Physics.Raycast(transform.position,transform.TransformDirection(Vector3.right), distance[1], ~enemy);

        forward = Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward), distance[2], ~enemy);
        backward = Physics.Raycast(transform.position,transform.TransformDirection(Vector3.back), distance[3], ~enemy);
    }
    //protected void limitCheckAll(int[] distance) {
    //    limitCheck(distance);
    //    up = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), distance[4], ~enemy);
    //    down = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), distance[5], ~enemy);
    //}
    protected void targetDelay()
    {
        target = player.transform.position;
    }
    protected void RandomXZ() {
        if (Random.Range(-1, 1) > 0)
        {
            x = Random.Range(-1, 1);
        }
        else { z = Random.Range(-1, 1); }
    }
}
