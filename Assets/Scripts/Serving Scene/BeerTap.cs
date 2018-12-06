using Serving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerTap : MonoBehaviour {

	[SerializeField]
	Ingredient ingredient;

	[SerializeField]
	Pouring particles;

	private void Awake()
	{
		particles.ingredient = ingredient;
	}
}
