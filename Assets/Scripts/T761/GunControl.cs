using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunControl : MonoBehaviour
{
    public Camera cam;
    public GameObject gunPoint, CamPoint, emitter, Laser,LaserBurst;
    public Animator CoreAnime;
    public float fireRate;
    public Image laserBar;

    [HideInInspector] public bool aimMode;

    private GameObject current,previous;

    private Vector3 Hit,Direction;
    private bool wallRunning,itsDevice;
    private int onWall;
    private float nextFire,viewDistance;

    float currentFireStrength;
    private void Start()
    {
        viewDistance = 500;
        laserBar.enabled = false;
    }
    void Update()
    {
        wallRunning = GetComponentInParent<PlayerController>().wallRunning;
        onWall = GetComponentInParent<Animator>().GetInteger("wallRun");
        AllInputs();
    }
    private void FixedUpdate()
    {
        CHRay();
        
    }
    void AllInputs()
    {
        if (Input.GetMouseButtonDown(1) && !wallRunning)
        {
            aimMode = !aimMode;
            laserBar.enabled = aimMode;
            CoreAnime.SetBool("aimOn", aimMode);
            CamPoint.GetComponent<TPCamera>().aim = aimMode;
        }

        if (Input.GetMouseButton(0))
        {
            if (aimMode  && Time.time > nextFire && currentFireStrength>0)
            {
                GetComponent<Life>().currentLife -= 1;
                nextFire = Time.time + fireRate;
                Laser.GetComponent<Beams>().direction = Direction;
                Instantiate(Laser, emitter.transform.position, Quaternion.Euler(transform.eulerAngles));
                Instantiate(LaserBurst, emitter.transform.position, Quaternion.Euler(transform.eulerAngles));
            }
            else if (itsDevice && !aimMode)
            {
                current.GetComponent<Devices>().hack = true;
            }
        }
        else {

            if (current != null)
            {
                current.GetComponent<Devices>().hack = false;
            }
        }

        if (!itsDevice) {
            if (previous != null && current != null)
            {
                previous.GetComponent<Devices>().inFocus = false;
                current.GetComponent<Devices>().inFocus = false;
                current = previous = null;
            }
        }
        CamPoint.GetComponent<TPCamera>().reset = Input.GetMouseButton(2);
        CamPoint.GetComponent<TPCamera>().wallRun = wallRunning;
        CamPoint.GetComponent<Animator>().SetBool("boost",
            GetComponentInParent<PlayerController>().boosting);

        CoreAnime.SetInteger("onWall", onWall);

        currentFireStrength = GetComponent<Life>().currentLife;

        transform.position = gunPoint.transform.position;
        transform.rotation = CamPoint.transform.rotation;
    }
    void CHRay() {
        Vector3 ray = cam.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, cam.transform.forward, out hit, viewDistance))
        {
            // for not hitting itself
            Hit = hit.point;

            // for hacking
            if (hit.collider.gameObject.CompareTag("Device"))
            {
                itsDevice = true;
                current = hit.collider.gameObject;
                current.GetComponent<Devices>().inFocus = true;
                current.GetComponent<Devices>().player = transform.parent.gameObject;
                if (previous != null)
                {
                    if (previous != current)
                    {
                        previous.GetComponent<Devices>().inFocus = false;

                    }
                }
                previous = current;
            }
            else { 
                itsDevice = false;  
            }
        }
        else { 
            Hit = ray + (cam.transform.forward * viewDistance);
            itsDevice = false;
        }

        if (aimMode) {
            Direction = Hit - emitter.transform.position;
        }

    }

}
