using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serving
{
	[CreateAssetMenu(menuName = "Serving/New Ingredient")]
	public class Ingredient : ScriptableObject
	{
		public DrinkType type;
		public Color colour;

		[HideInInspector]
		public float timeToAdd = 1;
	}
}
