using UnityEngine;

/// <summary>
/// This should be added to the knives/axes object that has a rigidbody.
/// 
/// When the fixed joint is broken, the rigidbody has to return to its original settings.
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class BreakFixedJoint : MonoBehaviour {
    Rigidbody myObjectRB;

  /*  private void Start()
    {
        myObjectRB = GetComponent<Rigidbody>();
    }


    void OnJointBreak(float breakForce)
    {
        Debug.Log("A joint from " + gameObject.name + " has just been broken!, force: " + breakForce);

        BreakJoint();
    }

    public void BreakJoint()
    {

        myObjectRB.useGravity = true;
        myObjectRB.constraints = RigidbodyConstraints.FreezeRotationY;
        myObjectRB.isKinematic = false;
    }
    */

}
