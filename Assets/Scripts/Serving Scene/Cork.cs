﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cork : MonoBehaviour {

	FixedJoint joint;
	MeshCollider collider;
	Transform parent;

	private void Start()
	{
		
		joint = GetComponent<FixedJoint>();
		collider = GetComponent<MeshCollider>();
		collider.enabled = true;
		parent = transform.parent;

	}

	private void OnJointBreak(float breakForce)
	{
		if(transform.parent == parent)
		{
			transform.parent = null;
		}
	}

}
