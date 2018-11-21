using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serving
{
	[CreateAssetMenu(menuName = "Serving/New Recipe List")]
	public class RecipeList : ScriptableObject
	{

		[SerializeField]
		CompleteDrink[] allDrinks;

		// check to see how similar the ingredients of the required drink and the made drink are
		public float CheckDrink(CompleteDrink drink, int drinkNumber)
		{
			int score = 0;
			CompleteDrink completeDrink = allDrinks[drinkNumber];

			for (int i = 0; i < drink.usedIngredients.Length; i++)
			{
				Ingredient check = drink.usedIngredients[i];
				int scoreToAdd = -5;
				for (int j = 0; j < completeDrink.usedIngredients.Length; j++)
				{
					if (check == completeDrink.usedIngredients[j])
					{
						scoreToAdd = 10;
						j = completeDrink.usedIngredients.Length;
					}
					else if (check.type == completeDrink.usedIngredients[j].type)
					{
						scoreToAdd = 5;
						j = completeDrink.usedIngredients.Length;
					}
				}
				score += scoreToAdd;
			}

			if(drink.mixMethod == completeDrink.mixMethod)
			{
				score += 10;
			}
			else
			{
				score -= 5;
			}

			if(drink.glass == completeDrink.glass)
			{
				score += 10;
			}
			else
			{
				score -= 5;
			}
			
			int possibleScore = 20 + allDrinks[drinkNumber].usedIngredients.Length * 10;
			return (float)score/possibleScore;
		}
	}
}