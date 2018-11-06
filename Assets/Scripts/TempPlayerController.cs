using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerController : MonoBehaviour {

    public float speed;
    public float aimSpeed;
    public float sensitivity;
    Camera mainCamera;

    public Gun gun;
    [HideInInspector] public Vector3 gunPos;
    [HideInInspector] public Quaternion gunRot;

    void Start () {
        mainCamera = FindObjectOfType<Camera>();
        gun.aim = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += ((transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"))) * speed * Time.deltaTime;
        transform.eulerAngles += new Vector3(0f, Input.GetAxis("Mouse X"), 0f) * sensitivity * Time.deltaTime;
        mainCamera.transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), 0f, 0f) * sensitivity * Time.deltaTime;

        gun.transform.position = Vector3.Lerp(gun.transform.position, gunPos , aimSpeed * Time.deltaTime);
        gun.transform.rotation = Quaternion.Lerp(gun.transform.rotation, gunRot, aimSpeed * Time.deltaTime);

        if (gun.aim)
        {
            gunPos = gun.aimPos.position;
            gunRot = gun.aimPos.rotation;
        }
        else
        {
            gunPos = gun.hipPos.position;
            gunRot = gun.hipPos.rotation;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            gun.aim = gun.aim ? false : true;
        }
	}
}
