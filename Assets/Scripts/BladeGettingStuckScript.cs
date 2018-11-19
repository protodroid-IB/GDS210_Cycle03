using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeGettingStuckScript : MonoBehaviour
{
    [SerializeField] GameObject myObject;

    Rigidbody myObjectRB;

    private void Awake()
    {
        myObjectRB = myObject.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ThrowingTarget")
        {
            myObjectRB.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ThrowingTarget")
        {
            myObjectRB.constraints = RigidbodyConstraints.None;
        }
    }
}
