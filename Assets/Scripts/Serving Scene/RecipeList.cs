using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serving
{
	[CreateAssetMenu(menuName = "Serving/New Recipe List")]
	public class RecipeList : ScriptableObject
	{

		public CompleteDrink[] allDrinks;

		// check to see how similar the ingredients of the required drink and the made drink are
		public int CheckDrink(CompleteDrink drink, int drinkNumber)
		{
			int score = 0;
			CompleteDrink completeDrink = allDrinks[drinkNumber];


			if(completeDrink.usedIngredient == drink.usedIngredient)
			{
				score +=(int) (50 * drink.ingredientAmount);

			} else if(completeDrink.usedIngredient.type == drink.usedIngredient.type)
			{
				score += (int)(20 * drink.ingredientAmount);
			}

			if(completeDrink.glass == drink.glass)
			{
				score += 20;
			}

			return score;

			/*for (int i = 0; i < drink.usedIngredient.Length; i++)
			{
				Ingredient check = drink.usedIngredient[i];
				int scoreToAdd = -5;
				for (int j = 0; j < completeDrink.usedIngredient.Length; j++)
				{
					if (check == completeDrink.usedIngredient[j])
					{
						scoreToAdd = 10;
						j = completeDrink.usedIngredient.Length;
					}
					else if (check.type == completeDrink.usedIngredient[j].type)
					{
						scoreToAdd = 5;
						j = completeDrink.usedIngredient.Length;
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
			
			int possibleScore = 20 + allDrinks[drinkNumber].usedIngredient.Length * 10;
			return (float)score/possibleScore;*/
		}
	}
}