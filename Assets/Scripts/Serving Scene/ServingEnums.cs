using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Serving
{
	public enum MixMethod {
		Mixed, Stirred, Shaken
	}

	public enum DrinkType
	{
		Spirit, Beer, Wine, Mix, Flavour, Ice
	}

	[System.Serializable]
	public struct CompleteDrink
	{
		public Ingredient[] usedIngredients;
		public MixMethod mixMethod;
	}

	

	
}
