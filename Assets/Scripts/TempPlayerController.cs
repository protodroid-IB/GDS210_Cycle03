using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerController : MonoBehaviour {

    public float speed;
    public float sensitivity;
    Camera mainCamera;

    public Gun gun;
    [HideInInspector] bool triggerDown = false;

    void Start () {
        Cursor.lockState = CursorLockMode.Confined;
        mainCamera = FindObjectOfType<Camera>();
        gun.aim = false;
	}

    // Update is called once per frame
    void Update()
    {
        transform.position += ((transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"))) * speed * Time.deltaTime;
        transform.eulerAngles += new Vector3(0f, Input.GetAxis("Mouse X"), 0f) * sensitivity * Time.deltaTime;
        mainCamera.transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), 0f, 0f) * sensitivity * Time.deltaTime;

        if (!triggerDown)
        {
            if (gun.primed)
            {
                if (Input.GetButton("Fire1"))
                {
                    gun.Fire();
                    triggerDown = true;
                    gun.hammerPull = 0;
                }
            }
            else
            {
                if (Input.GetButton("Fire1"))
                {
                    gun.hammerPull = -0.01f;
                }
                else
                {
                    gun.hammerPull = Input.GetAxis("Mouse ScrollWheel");
                }
            }
        }
        else if (!Input.GetButton("Fire1"))
        {
            triggerDown = false;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            gun.aim = gun.aim ? false : true;
        }
    }
}
