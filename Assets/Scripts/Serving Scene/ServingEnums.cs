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

	public enum GlassType
	{
		WineGlass, BeerGlass, Tumbler
	}

	[System.Serializable]
	public struct CompleteDrink
	{
		public Ingredient usedIngredient;
		public float ingredientAmount;
		//public MixMethod mixMethod;
		public GlassType glass;
        public string audioClipName;
	}
	
}
