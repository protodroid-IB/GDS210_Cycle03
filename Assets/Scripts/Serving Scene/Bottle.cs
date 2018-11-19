using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serving;

public class Bottle : MonoBehaviour {

	[SerializeField]
	float bottleLean, emissionAmount;

	[SerializeField]
	float leanTolerance;

	Pouring pourEffect;

	Wobble liquidEffect;

	bool cork;

	// Use this for initialization
	void Start () {
		pourEffect = GetComponentInChildren<Pouring>();
		liquidEffect = GetComponentInChildren<Wobble>();
	}
	
	// Update is called once per frame
	void Update () {
		bottleLean = Vector3.Dot(Vector3.up, transform.up);
		emissionAmount = Mathf.Clamp((leanTolerance - bottleLean)*2f, 0, 1);
		pourEffect.strength = emissionAmount;
	}

}
