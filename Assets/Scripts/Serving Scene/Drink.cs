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

		//List<DrinkType> types = new List<DrinkType>();

		MixMethod method = MixMethod.Mixed;

		public float ingredientAdded;

		[SerializeField]
		Pouring particles;

		Wobble wobble;

		bool clear;

		private void Start()
		{
			wobble = GetComponent<Wobble>();
			currentIngredient = FindObjectOfType<Ingredient>();
		}

		private void Update()
		{
			float tilt = Vector3.Dot(Vector3.up, transform.up);
			if(tilt <= 0 && !clear)
			{
				clear = true;
				if (particles)
				{
					particles.strength = (tilt * (-1));
					particles.ingredient = currentIngredient;
				}
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
			if (currentIngredient == ingredient && ingredientAdded >= 1)
				return;

			if(ingredient != currentIngredient)
			{
				ingredientAdded = time;
				currentIngredient = ingredient;
				wobble.ingredient = ingredient;
				wobble.SetColours();
			}
			else
			{
				ingredientAdded = Mathf.Clamp01(ingredientAdded + time);
				wobble.fillLevel = ingredientAdded;
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
				ingredientAmount = ingredientAdded,
				//mixMethod = method,
				glass = glassType
			};
			return drink;
		}
	}
}
