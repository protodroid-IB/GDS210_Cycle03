using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{

    Gun gunScript;

    [SerializeField]
    float upVelocity;

    [SerializeField]
    float rightVelocity;

    [SerializeField]
    float leftVelocity;

    public Vector3 originalPos;

    float lastAngle;

    public GameObject gunObject;
    public GameObject gunCylinder;

    private void Start()
    {

        gunScript = gunObject.GetComponent<Gun>();

    }

    private void Update()
    {

        if(gunCylinder.transform.localPosition != new Vector3(0f, 0f, 0f))
        {

            gunCylinder.transform.localPosition = new Vector3(0f, 0f, 0f);

        }

        if (gunScript.loaded == true)
        {

            gunCylinder.transform.localEulerAngles = originalPos;
            gunCylinder.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }
        else
        {

            gunCylinder.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeRotationY;

        }
    }

    public void SpinCylinder(float scroll, bool angle = true)
    {
        if (angle)
        {
            SpinCylinder(Mathf.DeltaAngle(scroll, lastAngle));
        }
        else
        {
            SpinCylinder(scroll);
        }
    }

    void SpinCylinder(float scroll)
    {

        
        if (scroll > 0f)
        {
            //scrolls up
            //gunCylinder.GetComponent<Rigidbody>().AddTorque(transform.up * upVelocity);
            //gunCylinder.GetComponent<Rigidbody>().AddTorque(transform.right * rightVelocity);

            gunCylinder.transform.Rotate(new Vector3(0, 1, 0), scroll * rightVelocity);

        }

        if (scroll < 0f)
        {
            //scrolls down
            // gunCylinder.GetComponent<Rigidbody>().AddTorque(transform.up * -upVelocity);
            // gunCylinder.GetComponent<Rigidbody>().AddTorque(transform.right * -leftVelocity);

            gunCylinder.transform.Rotate(new Vector3(0, 1, 0), scroll * leftVelocity);

        }

    }

}

