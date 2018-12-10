using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hubworld : MonoBehaviour {
    Hubworld hubworld;

	// Use this for initialization
	void Awake () {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else hubworld = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
