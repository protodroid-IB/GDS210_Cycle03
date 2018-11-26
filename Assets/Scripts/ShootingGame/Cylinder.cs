using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    public GameObject cylinder;
    public float cylinderDrag;

    Gun gun;
    float lastAngle = 0f;
    [HideInInspector] public float cylinderTorque = 0f;

    float rotationSpeed = 2;

    private void Start()
    {
        gun = GetComponentInParent<Gun>();
    }

    private void Update()
    {
        
        Quaternion newRotation = cylinder.transform.localRotation;
        newRotation *= Quaternion.AngleAxis(cylinderTorque * rotationSpeed, Vector3.up);
        cylinder.transform.localRotation = newRotation;
        
        //cylinder.transform.localEulerAngles = localEulers;
            //Rotate(Vector3.up, cylinderTorque * Time.deltaTime);
        cylinderTorque = Mathf.Lerp(cylinderTorque, 0f, cylinderDrag * Time.deltaTime);
    }

    public void SpinCylinder(float scroll, bool angle = true)
    {
        if (angle)
        {
            SpinCylinder(Mathf.DeltaAngle(scroll, lastAngle));
            lastAngle = scroll;
        }
        else
        {
            SpinCylinder(scroll * 20);
        }
    }

    void SpinCylinder(float scroll)
    {
        print(scroll);

        if (scroll != 0)
            cylinderTorque = scroll;
        //cylinderTorque = scroll * gun.cylinderSensitivity;
    }
}

