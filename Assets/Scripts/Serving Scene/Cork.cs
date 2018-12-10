using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cork : MonoBehaviour {

	FixedJoint joint;
	MeshCollider collider;
	Transform parent, overParent;

	private void Start()
	{
		
		joint = GetComponent<FixedJoint>();
		collider = GetComponent<MeshCollider>();
		collider.enabled = true;
		parent = transform.parent;
		overParent = GameObject.FindGameObjectWithTag("Parent").transform;
	}

	private void Update()
	{
		if(joint && transform.parent != parent)
		{
			Destroy(joint);
		}
	}

	private void OnJointBreak(float breakForce)
	{
		if(transform.parent == parent)
			transform.parent = overParent;
	}

}
