using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerController : MonoBehaviour {

    public float speed;
    public float sensitivity;
    public float aimSpeed;
    public float triggerSpeed;

    Camera mainCamera;

    public Gun gun;
    public Transform hipPos;
    public Transform aimPos;
    [HideInInspector] public bool aim;
    Vector3 gunPos;
    Quaternion gunRot;

    float triggerPull;

    void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = FindObjectOfType<Camera>();
        aim = false;
	}
    
    void Update()
    {
        transform.position += ((transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"))) * speed * Time.deltaTime;
        transform.eulerAngles += new Vector3(0f, Input.GetAxis("Mouse X"), 0f) * sensitivity * Time.deltaTime;
        mainCamera.transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), 0f, 0f) * sensitivity * Time.deltaTime;
        var scroll = -Input.GetAxis("Mouse ScrollWheel");
        gun.TriggerPull(triggerPull);

        if (gun.loaded)
        {
            if (Input.GetButton("Fire1"))
            {
                triggerPull = Mathf.MoveTowards(triggerPull, 1f, triggerSpeed * Time.deltaTime);
            }
            else
            {
                triggerPull = 0f;
                gun.HammerPull(aim ? scroll : scroll * 10);
            }
        }
        else
        {
            if (scroll != 0)
            {
                gun.cylinder.SpinCylinder(scroll, false);
            }
        }

        if (Input.GetButtonDown("Reload"))
        {
            gun.Reload();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            aim = aim ? false : true;
        }

        if (aim)
        {
            gunPos = aimPos.position;
            gunRot = aimPos.rotation;
        }
        else
        {
            gunPos = hipPos.position;
            gunRot = hipPos.rotation;
        }

        gun.transform.position = Vector3.Lerp(gun.transform.position, gunPos, aimSpeed * Time.deltaTime);
        gun.transform.rotation = Quaternion.Lerp(gun.transform.rotation, gunRot, aimSpeed * Time.deltaTime);
    }
}
