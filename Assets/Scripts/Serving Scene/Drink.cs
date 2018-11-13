using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Serving;

namespace Serving
{
	// used to manage and create a drink
	public class Drink : MonoBehaviour
	{

		// list of main ingredients added
		List<Ingredient> ingredients = new List<Ingredient>();

		MixMethod method = MixMethod.Mixed;

		public void AddIngredient(Ingredient ingredient)
		{
			if (!ingredients.Contains(ingredient))
			{
				ingredients.Add(ingredient);
				ingredients = ingredients.OrderBy(t => t.name).ToList();
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
