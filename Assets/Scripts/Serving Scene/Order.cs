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

		float timer = 0;

		float roundTime = 30;

		bool gameInProgress, orderBox;

		private void Start()
		{
			manager = FindObjectOfType<ServingGameManager>();
			GetOrder();
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
				timer += Time.deltaTime;
				if(timer >= roundTime)
				{
					gameInProgress = false;
					timer = 0;
				}
			}
			else if(orderBox)
			{ 
				orderText.transform.parent.gameObject.SetActive(false);
				orderBox = false;
			}
		}

		public void StartGame(float newRoundTime)
		{
			gameInProgress = true;
			roundTime = newRoundTime;
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
			/*for (int i = 0; i < drink.usedIngredient.Length; i++)
			{
				text += drink.usedIngredient[i].name;
				text += "\n";
			}
			text += drink.mixMethod.ToString();*/
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

		private void OnCollisionEnter(Collision collision)
		{
			Drink recievedDrink = collision.gameObject.GetComponentInChildren<Drink>();
			if (recievedDrink)
			{
				int score = (int)(CheckDrink(recievedDrink.GetDrink()) * maxScoreAmount);
				Score(score);
			}
			Destroy(collision.gameObject);
		}
	}
}
