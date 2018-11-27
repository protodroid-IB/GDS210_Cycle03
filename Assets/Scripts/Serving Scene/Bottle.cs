using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serving
{
	public class Bottle : MonoBehaviour {

		Ingredient ingredient;

		private void Awake()
		{
			Pouring emitter = GetComponentInChildren<Pouring>();
			Wobble wobbler = GetComponentInChildren<Wobble>();

			emitter.ingredient = ingredient;
			wobbler.ingredient = ingredient;
		}
	}
}
