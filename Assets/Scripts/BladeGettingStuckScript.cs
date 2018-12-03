using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeGettingStuckScript : MonoBehaviour
{
    [SerializeField] GameObject myObject;

    Rigidbody myObjectRB;

    ThrowingGameController tgc;
    DidIHitTarget diht;

    FixedJoint myFixedJoint;
    
    [Tooltip("How deep the blade will stick into a target.")]
    [SerializeField] float depth;

    [Tooltip("Force required to break the FixedJoint")]
    [SerializeField] int breakForce;

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


        if (other.gameObject.tag == "ThrowingTarget")
        {
            ParentMe(other, depth);     // Sticks to targets, and penetrates.

            if (!diht.AmIUsed())
            {
                print(other.name);
                diht.SetUsed();
                tgc.AddScore(100);
            }
            
        }
        else if(other.gameObject.tag == "Floor")
        {
            // Sticks to any other collider but does not penetrates. Stops falling through floor.
            // Enables kinematic to stop parents collider bouncing away from others collider.
            ParentMe(other, 0);    
            myObjectRB.isKinematic = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // Ensure the rigidbody is not kinematic when pulled away from other colliders.
        myObjectRB.isKinematic = false;
    }

    private void ParentMe(Collider other, float penetrateDepth)
    {

        // Stick the blade into the target, using depth as a measure to how far it sticks in.
        myObject.transform.Translate(myObjectRB.velocity.normalized * Time.deltaTime * -penetrateDepth);


        // Adjust Rigidbody to account for being stuck in a target, especially the moving target.
        myObjectRB.velocity = Vector3.zero;
        myObjectRB.useGravity = false;
        myObjectRB.constraints = RigidbodyConstraints.None;

        // Create the fixed joint.
        myFixedJoint = myObject.AddComponent<FixedJoint>();
        myFixedJoint.connectedBody = other.GetComponent<Rigidbody>();
        myFixedJoint.breakForce = breakForce;

    }

}
