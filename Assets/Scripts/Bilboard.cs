using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour {

	Transform mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 forwardPosition = transform.position - mainCamera.position;
		forwardPosition = Vector3.Normalize(forwardPosition);
		transform.forward = forwardPosition;
			
			//.LookAt(mainCamera.position, Vector3.up);
	}
}
