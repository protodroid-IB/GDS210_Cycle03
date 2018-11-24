using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Serving
{
	public class Order : MonoBehaviour
	{
		[SerializeField]
		RecipeList recipes;

		[SerializeField]
		TextMeshPro orderText;

		int maxScoreAmount;

		int order = 0;

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				GetOrder();
			}
		}

		public void GetOrder()
		{
			int numberOfDrinks = recipes.allDrinks.Length;
			order = Random.Range(0, numberOfDrinks);
			SetText();
		}

		public CompleteDrink GetDrink()
		{
			return recipes.allDrinks[order];
		}

		void SetText()
		{
			string text = "";
			CompleteDrink drink = GetDrink();
			for (int i = 0; i < drink.usedIngredients.Length; i++)
			{
				text += drink.usedIngredients[i].name;
				text += "\n";
			}
			text += drink.mixMethod.ToString();
			orderText.text = text;
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
