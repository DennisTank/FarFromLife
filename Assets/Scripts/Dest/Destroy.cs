using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float destroyInSecs;
    void Start()
    {
        Invoke("Dest",destroyInSecs);
    }

    void Dest() { Destroy(gameObject); }
}
