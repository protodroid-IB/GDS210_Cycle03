using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeGettingStuckScript : MonoBehaviour
{
    [SerializeField] GameObject myObject;

    Rigidbody myObjectRB;

    ThrowingGameController tgc;
    DidIHitTarget diht;

    private void Awake()
    {
        myObjectRB = myObject.GetComponent<Rigidbody>();

        tgc = GameObject.Find("ThrowingGameController").GetComponent<ThrowingGameController>();
        diht = myObjectRB.GetComponent<DidIHitTarget>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 2)
        {
            return;
        }
        
        myObjectRB.constraints = RigidbodyConstraints.FreezeAll;

        if (other.gameObject.tag == "ThrowingTarget")
        {
            myObject.GetComponent<Transform>().SetParent(other.gameObject.transform, true);

            if(!diht.AmIUsed())
            {
                diht.SetUsed();
                tgc.AddScore(100);
            }
            
        }
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
