using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Serving
{
	public class Order : MonoBehaviour
	{
		[SerializeField]
		RecipeList recipes;

		int maxScoreAmount;

		int order = 0;

		public void GetOrder()
		{
			int numberOfDrinks = recipes.allDrinks.Length;
			order = Random.Range(0, numberOfDrinks);
		}

		public CompleteDrink GetDrink()
		{
			return recipes.allDrinks[order];
		}

		float CheckDrink(CompleteDrink drink)
		{
			return recipes.CheckDrink(drink, order);
		}

		void Score(int scoreAmount)
		{
			GameManager.score += scoreAmount;
		}

		private void OnCollisionEnter(Collision collision)
		{
			Drink recievedDrink = collision.gameObject.GetComponentInChildren<Drink>();
			if (recievedDrink)
			{
				int score = (int)(CheckDrink(recievedDrink.GetDrink()) * maxScoreAmount);
				Score(score);
			}
		}
	}
}
