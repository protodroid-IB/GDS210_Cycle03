using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cork : MonoBehaviour {

	[SerializeField]
	GameObject corkObject;

	[SerializeField]
	Transform corkTransform;

	// Use this for initialization
	void Start () {
		GameObject cork = Instantiate(corkObject, corkTransform.position, corkTransform.rotation);
		cork.transform.localScale *= 9.5f;
		FixedJoint joint = cork.GetComponent<FixedJoint>();
		joint.connectedBody = GetComponent<Rigidbody>();
	}

}
