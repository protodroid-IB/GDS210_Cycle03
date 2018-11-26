using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serving
{
	public class LeanPourEffect : MonoBehaviour
	{

		[SerializeField]
		float lean, emissionAmount;

		[SerializeField]
		float leanTolerance;

		[SerializeField]
		Pouring pourEffect;


		// Update is called once per frame
		void Update()
		{
			lean = Vector3.Dot(Vector3.up, transform.up);
			emissionAmount = Mathf.Clamp((leanTolerance - lean) * 2f, 0, 1);
			pourEffect.strength = emissionAmount;
		}

	}
}
