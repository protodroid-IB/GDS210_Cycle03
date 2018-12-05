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
		/*[SerializeField]
		List<Ingredient> ingredients = new List<Ingredient>();*/

		Ingredient currentIngredient;

		[SerializeField]
		GlassType glassType = GlassType.BeerGlass;

		Ingredient adding;

		//List<DrinkType> types = new List<DrinkType>();

		MixMethod method = MixMethod.Mixed;

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
					currentIngredient = null;
				}
				wobble.fillLevel = Mathf.Clamp01(wobble.fillLevel - Time.deltaTime);
			}
		}

		public void AddIngredient(Ingredient ingredient, float time)
		{
			if (currentIngredient == ingredient)
				return;

			if(ingredient != adding)
			{
				ingredientAdded = time;
				adding = ingredient;
				wobble.ingredient = ingredient;
				wobble.SetColours();
			}
			else
			{
				ingredientAdded += time;
				wobble.fillLevel = ingredientAdded;

				if (ingredientAdded >= 1)
				{
					currentIngredient = adding;
					/*types.Add(ingredient.type);
					ingredients.Add(ingredient);
					ingredients = ingredients.OrderBy(t => t.name).ToList();*/
				}
			}
		}

		/*public void Combine(MixMethod mixMethod)
		{
			if (mixMethod > method)
			{
				method = mixMethod;
			}
		}*/

		public CompleteDrink GetDrink()
		{
			CompleteDrink drink = new CompleteDrink
			{
				usedIngredient = currentIngredient,
				//mixMethod = method,
				glass = glassType
			};
			return drink;
		}
	}
}
