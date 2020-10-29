using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeTurnOff : MonoBehaviour
{
    public Animator target;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            target.GetComponent<Animator>().SetBool("on",false);
            Destroy(gameObject);
        }
    }

}
