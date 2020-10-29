using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Beams : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject hitBust;
    [HideInInspector] public Vector3 direction;

    Rigidbody rg;
    private void Awake()
    {
        rg = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rg.AddForce(direction * bulletSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Life>() != null) {
            GetComponent<Damage>().Now(other.gameObject.GetComponent<Life>());
        }

        Instantiate(hitBust, transform.position, Quaternion.Euler(transform.eulerAngles));
        Dest();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Life>() != null)
        {
            GetComponent<Damage>().Now(other.gameObject.GetComponent<Life>());
        }

        Instantiate(hitBust, transform.position, Quaternion.Euler(transform.eulerAngles));
        Dest();
    }
    private void Dest()
    {
        Destroy(gameObject);
    }
}
