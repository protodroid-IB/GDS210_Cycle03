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

		[SerializeField]
		Transform textSpawnPosition;

		TextMeshPro scoreText;

		[SerializeField]
		GameObject scoreTextObject;

		ServingGameManager manager;

		int order = 0;

		public bool gameInProgress;

		bool orderBox = true;

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

		int CheckDrink(CompleteDrink drink)
		{
			return recipes.CheckDrink(drink, order);
		}

		void Score(int scoreAmount)
		{
			manager.AddScore(scoreAmount);
			scoreText = Instantiate(scoreTextObject, textSpawnPosition.position, Quaternion.identity, orderText.transform).GetComponent<TextMeshPro>();
			scoreText.text = scoreAmount.ToString() + " Points";
			GetOrder();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag != "Glass")
				return;
			if (gameInProgress)
			{
				Drink recievedDrink = other.gameObject.GetComponentInChildren<Drink>();
				if (recievedDrink)
				{
					int score = CheckDrink(recievedDrink.GetDrink());
					Score(score);
				}
			}
			Destroy(other.gameObject);
		}
	}
}
