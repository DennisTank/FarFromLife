using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparks : MonoBehaviour
{
    public Material m;
    public AudioClip[] a;

    void Start()
    {
        GetComponent<AudioSource>().clip = clip();
        GetComponent<AudioSource>().Play();
        m.SetColor("Color_6CB8A494", rc()/2);
        m.SetColor("Color_45B5B2C", rc());
    }
    Color rc() {
        return new Color(
            Random.Range(0.0f,1.0f),
            Random.Range(0.0f,1.0f),
            Random.Range(0.0f,1.0f)
            );
    }
    AudioClip clip() {
        return (Random.Range(0.0f,10.0f) < 3)? a[0]:
            (Random.Range(0.0f, 10.0f) > 7)?a[1]:a[2];
    }
    
}
