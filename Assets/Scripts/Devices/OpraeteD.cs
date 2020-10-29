using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpraeteD : Devices
{
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

    void Update()
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
            if (!hacked && currentHackStrength > 0)
            {
                currentHackStrength -= 0.05f;
            }
            line.enabled = false; 
        }
        if (currentHackStrength > hackStrength- 0.1f && !hacked) {
            currentHackStrength = hackStrength;
            target.GetComponent<Animator>().SetBool("on", true);
            hacked = true;
        }

        fillBar.fillAmount = ((currentHackStrength) / hackStrength);
        currentHackStrength = Mathf.Clamp(currentHackStrength, 0, hackStrength);

        lockID.enabled = inFocus;
    }
}
