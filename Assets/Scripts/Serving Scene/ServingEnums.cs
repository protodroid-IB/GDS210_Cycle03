﻿using System.Collections;
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

	[CreateAssetMenu(menuName = "Serving/New Recipe List")]
	public class RecipeList : ScriptableObject
	{

		[SerializeField]
		CompleteDrink[] allDrinks;

		// check to see how similar the ingredients of the required drink and the made drink are
		public int CheckDrink(CompleteDrink drink, int drinkNumber)
		{
			int score = 0;
			CompleteDrink completeDrink = allDrinks[drinkNumber];

			for (int i = 0; i < drink.usedIngredients.Length; i++)
			{
				Ingredient check = drink.usedIngredients[i];
				for (int j = 0; j < completeDrink.usedIngredients.Length; j++)
				{
					if(check == completeDrink.usedIngredients[j])
					{
						score += 10;
						j = completeDrink.usedIngredients.Length;
					}
					else if (check.type == completeDrink.usedIngredients[j].type)
					{
						score += 5;
						j = completeDrink.usedIngredients.Length;
					}
				}
			}
			return score;
		}
	}

	[CreateAssetMenu(menuName = "Serving/New Ingredient")]
	public class Ingredient : ScriptableObject
	{
		public DrinkType type;
		Color colour;
	}
}