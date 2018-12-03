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

		ServingGameManager manager;

		int maxScoreAmount;

		int order = 0;

		public bool gameInProgress;

		bool orderBox;

		private void Start()
		{
			manager = FindObjectOfType<ServingGameManager>();
			
		}

		private void Update()
		{
			if(gameInProgress)
			{
				if (!orderBox)
				{
					orderText.transform.parent.gameObject.SetActive(true);
					orderBox = true;
				}
			}
			else if(orderBox)
			{ 
				orderText.transform.parent.gameObject.SetActive(false);
				orderBox = false;
			}
		}

		public void StartGame()
		{
			gameInProgress = true;
			orderBox = true;
			GetOrder();
		}

		public void EndGame()
		{
			gameInProgress = false;
			orderBox = false;
		}

		void GetOrder()
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
			CompleteDrink drink = GetDrink();
			orderText.text = drink.usedIngredient.name;
		}

		float CheckDrink(CompleteDrink drink)
		{
			return recipes.CheckDrink(drink, order);
		}

		void Score(int scoreAmount)
		{
			manager.AddScore(scoreAmount);
			GetOrder();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (gameInProgress)
			{
				Drink recievedDrink = other.gameObject.GetComponentInChildren<Drink>();
				if (recievedDrink)
				{
					int score = (int)(CheckDrink(recievedDrink.GetDrink()) * maxScoreAmount);
					Score(score);
				}
			}
			Destroy(other.gameObject);
		}
	}
}
