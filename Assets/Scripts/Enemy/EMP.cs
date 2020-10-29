using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : MonoBehaviour
{
    GameObject target;
    private void OnTriggerEnter(Collider other)
    {      
        if (other.gameObject.CompareTag("Player")) {
            target = other.gameObject;
            InvokeRepeating("harm",0,1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CancelInvoke("harm");
            target = null;
        }
    }

    void harm() {
        GetComponent<Damage>().Now(target.gameObject.GetComponent<Life>());
    }

}
