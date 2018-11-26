using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeGettingStuckScript : MonoBehaviour
{
    [SerializeField] GameObject myObject;

    ThrowingGameController throwingGameController;

    Rigidbody myObjectRB;

    private void Awake()
    {
        throwingGameController = GameObject.Find("ThrowingGameController").GetComponent<ThrowingGameController>();

        myObjectRB = myObject.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 2)
        {
            return;
        }

        myObjectRB.constraints = RigidbodyConstraints.FreezeAll;

        if (other.gameObject.tag == "ThrowingTarget")
            throwingGameController.Addscore(100);
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 2)
        {
            return;
        }

        myObjectRB.constraints = RigidbodyConstraints.None;
    }
}
