using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class Devices : MonoBehaviour
{
    public float signalThreshold;
    public float hackStrength;
    public bool isAlb;
    public LineRenderer line;
    public Image fillBar,lockID;
    public Sprite loc, unloc;
    public Animator lockAnime;
    public AudioClip n_Alb, H_ing, H_ed;

    [HideInInspector] public bool inFocus, hack, hacked;
    [HideInInspector] public float signalStrength, currentHackStrength;
    [HideInInspector] public GameObject player;


    protected void DrawConnection(Vector3 start,Vector3 end)
    {
        lockAnime.SetBool("now",((!isAlb&&hack) || (hacked&&hack)));
        if ((!isAlb && hack)) {
            GetComponent<AudioSource>().loop = false;
            GetComponent<AudioSource>().clip = n_Alb;
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        else if ((hacked && hack)) {
            GetComponent<AudioSource>().loop = false;
            GetComponent<AudioSource>().clip = H_ed;
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        lockID.color = (isAlb) ? Color.cyan : Color.red;
        lockID.sprite = (hacked) ? unloc : loc;
        signalStrength = (Vector3.Distance(start, end) - (signalThreshold*10)) /(signalThreshold * 10);
        signalStrength = Mathf.Clamp(signalStrength,0,1);
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        line.material.SetFloat("Vector1_640557C0",signalStrength);
        line.material.SetColor("Color_10CBCEDC",(hacked)?Color.cyan:Color.red);
    }
    protected void inUse()
    {
        if (!inFocus) {
            GetComponent<AudioSource>().Stop();
            hack = false;
        }
        if (signalStrength == 0 && hack && isAlb && !hacked)
        {
            currentHackStrength += 0.05f;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().clip = H_ing;
            if (!GetComponent<AudioSource>().isPlaying) {
                GetComponent<AudioSource>().Play();
            }
        }
    }
 
}
