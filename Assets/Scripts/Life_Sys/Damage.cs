using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;

    public void Now(Life life) {
        life.currentLife -= damage;
    }
}
