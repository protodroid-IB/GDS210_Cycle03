using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serving
{
	
	public class Bottle : MonoBehaviour {

		[SerializeField]
		Ingredient ingredient;

		private void Awake()
		{
			Pouring emitter = GetComponentInChildren<Pouring>();
			Wobble wobbler = GetComponentInChildren<Wobble>();
			if(emitter)
				emitter.ingredient = ingredient;
			if(wobbler)
				wobbler.ingredient = ingredient;

			VRTK.InteractableObject interact = GetComponent<VRTK.InteractableObject>();
			if (!interact)
				gameObject.AddComponent<VRTK.InteractableObject>();
		}
	}
}
