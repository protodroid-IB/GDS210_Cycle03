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


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("KnifeCollider");
        if (other.tag == "ThrowingTarget")
        {
            myObjectRB.constraints = RigidbodyConstraints.FreezeAll;
            
            Debug.Log("FrozenKnife");
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("KnifeCollider");
        
        if (collision.gameObject.tag == "ThrowingTarget")
        {
            myObjectRB.constraints = RigidbodyConstraints.FreezeAll;
            
            Debug.Log("FrozenKnife");
        }
    }*/

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ThrowingTarget")
        {
            myObjectRB.constraints = RigidbodyConstraints.None;
        }
    }
}
