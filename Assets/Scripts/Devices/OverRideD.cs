using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverRideD : Devices
{
    [Tooltip("Is this device is reOverRideable")]
    public bool reOverRiding;
    [Tooltip("number of pre OverRiders")]
    public int reOverRideNum;
    public GameObject target;
    

    private void Awake()
    {
        hacked = false;
        inFocus = false;
        hack = false;
        currentHackStrength = 0;
        fillBar.fillAmount = 0;
        line.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (inFocus)
        {
            line.enabled = true;
            DrawConnection(transform.position, player.transform.position);
            if (Vector3.Distance(transform.position, player.transform.position) > 500) inFocus = false;
            inUse();
        }
        else {
            GetComponent<AudioSource>().Stop();
            if (!hacked && currentHackStrength>0) {
                currentHackStrength -= 0.05f;
            }    
            line.enabled = false; 
        }
        if (currentHackStrength > hackStrength-0.1f) {
            currentHackStrength = hackStrength;
            if (reOverRiding)
            {
                target.GetComponent<OverRideD>().reOverRideNum -= 1;
            }
            else { target.GetComponent<OpraeteD>().isAlb = true; }
            hacked = true;
        }
        if (reOverRideNum == 0) { isAlb = true; }
        else { isAlb = false; }

        fillBar.fillAmount = ((currentHackStrength) / hackStrength);
        currentHackStrength = Mathf.Clamp(currentHackStrength, 0, hackStrength);

        lockID.enabled = inFocus;
    }

}
