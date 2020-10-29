using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowerDrown : basicMovements
{
    public GameObject emitter,laser;

    private int[] dist;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        speed = 10;
        checkMoves = true;
        dist = new int[4]{3,3,3,3};
        x = z = y = 0;
        InvokeRepeating("targetDelay",0,1);
        InvokeRepeating("RandomXZ", 0,0.5f);
        InvokeRepeating("shoot",0,0.5f);
    }
    private void FixedUpdate()
    {
        limitCheck(dist);
    }
    private void Update()
    {
        if (GetComponent<Life>().currentLife<=0) {
            //inst
            CancelInvoke("targetDelay");
            CancelInvoke("RandomXZ");
            CancelInvoke("shoot");
            Destroy(gameObject);
        }
        LookAtPlayer();
        move();        
    }
    void LookAtPlayer()
    {

        transform.rotation = Quaternion.LookRotation(target - transform.position);
    }
    void shoot() {
        if (Random.Range(-5, 3) > 0) {
            laser.GetComponent<Beams>().direction = target - emitter.transform.position;
            Instantiate(laser, emitter.transform.position, emitter.transform.rotation);
        }
    }
}
