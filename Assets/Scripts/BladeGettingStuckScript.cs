using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeGettingStuckScript : MonoBehaviour
{
    [SerializeField] GameObject myObject;

    Rigidbody myObjectRB;

    ThrowingGameController tgc;

    private void Awake()
    {
        myObjectRB = myObject.GetComponent<Rigidbody>();

        tgc = GameObject.Find("ThrowingGameController").GetComponent<ThrowingGameController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 2)
        {
            return;
        }

        myObject.GetComponent<Transform>().SetParent(other.gameObject.transform,true);
        myObjectRB.isKinematic = false;

        if (other.gameObject.tag == "ThrowingTarget")
            tgc.AddScore(100);
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 2)
        {
            return;
        }

        myObjectRB.isKinematic = true;
        
    }
}
