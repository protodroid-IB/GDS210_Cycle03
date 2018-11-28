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

		[SerializeField]
		GlassType glassType = GlassType.BeerGlass;

		Ingredient adding;

		List<DrinkType> types = new List<DrinkType>();

		MixMethod method = MixMethod.Mixed;

		float timer;

		[SerializeField]
		float ingredientAdded;

		Wobble wobble;

		bool clear;

		private void Start()
		{
			wobble = GetComponent<Wobble>();
		}

		private void Update()
		{
			float tilt = Vector3.Dot(Vector3.up, transform.up);
			if(tilt <= 0 && !clear)
			{
				clear = true;
			}
			else if(tilt > 0 && clear)
			{
				clear = false;
			}

			if(clear)
			{
				if (wobble.fillLevel <= 0)
				{
					ingredients.Clear();
					types.Clear();
				}
				wobble.fillLevel = Mathf.Clamp01(wobble.fillLevel - Time.deltaTime);
			}
		}

		public void AddIngredient(Ingredient ingredient, float time)
		{
			if (ingredients.Contains(ingredient) || types.Contains(ingredient.type))
				return;
			if(ingredient != adding)
			{
				ingredientAdded = time;
				adding = ingredient;
				wobble.ingredient = ingredient;
			}
			else
			{
				timer += time;
				ingredientAdded = timer / ingredient.timeToAdd + 0.01f;
				wobble.fillLevel = ingredientAdded;

				if (ingredientAdded >= 1)
				{
					types.Add(ingredient.type);
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
				mixMethod = method,
				glass = glassType
			};
			return drink;
		}
	}
}
