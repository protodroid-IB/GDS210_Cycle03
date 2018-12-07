using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serving
{
	
	public class Bottle : MonoBehaviour {

		Vector3 startPosition;
		Quaternion startRotation;

		[SerializeField]
		Ingredient ingredient;

		private void Awake()
		{
			startPosition = transform.position;
			startRotation = transform.rotation;
			Pouring emitter = GetComponentInChildren<Pouring>();
			Wobble wobbler = GetComponentInChildren<Wobble>();
			if(emitter)
				emitter.ingredient = ingredient;
			if(wobbler)
				wobbler.ingredient = ingredient;

		}

        private void OnTriggerEnter(Collider other)
        {
            
        }

        private void OnCollisionEnter(Collision collision)
		{
			if(collision.transform.tag == "Floor")
			{
				transform.position = startPosition;
				transform.rotation = startRotation;
				GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
		}
	}
}
