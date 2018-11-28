using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour {

	Transform mainCamera;

	// Use this for initialization
	void Start () {

        Invoke("SetMainCamera", 1f);
	}
	
	// Update is called once per frame
	void Update () {
        if(mainCamera != null)
        {
		    Vector3 forwardPosition = transform.position - mainCamera.position;
		    forwardPosition = Vector3.Normalize(forwardPosition);
		    transform.forward = forwardPosition;
        }

			
			//.LookAt(mainCamera.position, Vector3.up);
	}

    void SetMainCamera()
    {
		mainCamera = Camera.main.transform;

    }
}
