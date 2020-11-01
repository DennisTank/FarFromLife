using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TPCamera : MonoBehaviour
{
    public GameObject Target, Cam;
    public float CenterHightOffset;
    public float Sensitivity;
    public LayerMask Player;

    public Animator crossHairUI;
    public Image dot, crossHair;

    private Vector2 xOrbitLimits; // x-> maxValue y-> minValue
    private Vector3 newPosition, newRotation;
    private float xMouse, yMouse;
    private bool isViewColl;

    [HideInInspector] public bool aim, wallRun, reset,canshoot;
    

    Animator anime;
    private void Awake()
    {
        anime = GetComponent<Animator>();
    }
    void Start()
    {
        xOrbitLimits = new Vector2(70,-50);
        newPosition = Target.transform.position;
        newRotation = Vector3.zero;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;// set cursor visibility->false in GM
    }
    private void Update()
    {
        CrossHair();
        
    }
    private void FixedUpdate()
    {
        viewCollision();
    }
    void LateUpdate()
    {
        Animations();
        SetPosition();

        if (reset) { CameraReset(); }
        else { SetRotation(); }

    }
    void CrossHair()
    {
        crossHairUI.SetBool("aim",aim);
        dot.color = (canshoot) ? Color.white : Color.gray;
        crossHair.color = (canshoot) ? Color.white : Color.gray;
    }
    void Animations() {
        anime.SetBool("Aim",aim|isViewColl);
        anime.SetBool("WallRunAim", wallRun);
    }
    void SetPosition() {
        newPosition.x = Target.transform.position.x;
        newPosition.z = Target.transform.position.z;
        newPosition.y = Target.transform.position.y + CenterHightOffset;
        transform.position = newPosition;
    }
    void SetRotation() {
        xMouse += Input.GetAxis("Mouse X") * Sensitivity;
        yMouse -= Input.GetAxis("Mouse Y") * Sensitivity;

        yMouse = Mathf.Clamp(yMouse, xOrbitLimits.y, xOrbitLimits.x);

        newRotation.x = yMouse;
        newRotation.y = xMouse;
        newRotation.z = 0;
        
        transform.rotation = Quaternion.Euler(newRotation);

    }
    void CameraReset() {
        float target = Target.transform.eulerAngles.y;
        float velocity = 0;

        xMouse = Mathf.SmoothDampAngle(transform.eulerAngles.y, target, ref velocity,Time.deltaTime,500);
        yMouse = Mathf.SmoothDampAngle(transform.eulerAngles.x,0,ref velocity,Time.deltaTime,500);

        newRotation.x = yMouse;
        newRotation.y = xMouse;
        newRotation.z = 0;

        transform.rotation = Quaternion.Euler(newRotation);
    }
    void viewCollision() {
        isViewColl = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), 5, ~Player);

    }
}
