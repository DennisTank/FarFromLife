using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blind_legs : basicMovements
{
    private int[] dist;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        speed = 10;
        checkMoves = true;
        dist = new int[4] { 10, 10, 10, 10 };
        y = 0;
        x = 1;
        z = -1;
        
    }
    private void FixedUpdate()
    {
        limitCheck(dist);
    }

    void Update()
    {
        move();
    }
}
