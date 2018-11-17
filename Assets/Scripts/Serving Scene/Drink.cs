using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Serving
{
	// used to manage and create a drink
	public class Drink : MonoBehaviour
	{

		// list of main ingredients added
		[SerializeField]
		List<Ingredient> ingredients = new List<Ingredient>();
		Ingredient adding;

		MixMethod method = MixMethod.Mixed;

		float timer;

		[SerializeField]
		float ingredientAdded;

		

		public void AddIngredient(Ingredient ingredient, float time)
		{
			if(ingredient != adding)
			{
				ingredientAdded = time;
				adding = ingredient;
			}
			else
			{
				timer += time;
				ingredientAdded = timer / ingredient.timeToAdd;
				if (ingredientAdded >= 1)
				{
					ingredients.Add(ingredient);
					ingredients = ingredients.OrderBy(t => t.name).ToList();
				}
			}
		}

		public void Combine(MixMethod mixMethod)
		{
			if (mixMethod > method)
			{
				method = mixMethod;
			}
		}

		public CompleteDrink GetDrink()
		{
			CompleteDrink drink = new CompleteDrink
			{
				usedIngredients = ingredients.ToArray(),
				mixMethod = method
			};
			return drink;
		}

		public void Clear()
		{
			ingredients.Clear();
		}
	}
}
