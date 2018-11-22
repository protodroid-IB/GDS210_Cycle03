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

    private void Start()
    {
        gun = GetComponentInParent<Gun>();
    }

    private void Update()
    {
        cylinder.transform.Rotate(Vector3.up, cylinderTorque * Time.deltaTime);
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
            SpinCylinder(scroll);
        }
    }

    void SpinCylinder(float scroll)
    {
        cylinderTorque = scroll * gun.cylinderSensitivity;
    }
}

