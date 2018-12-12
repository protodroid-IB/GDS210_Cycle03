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

        // Animator settings.
        Animator animator;
        [SerializeField] float minAnimSpeed;
        [SerializeField] float maxAnimSpeed;
        [SerializeField] Transform myDrinkingHand;
    

		int order = 0;

		public bool gameInProgress, freePlay;

		bool orderBox = true;



        private AudioSource orderAudio;

		private void Start()
		{
			manager = FindObjectOfType<ServingGameManager>();
            orderAudio = gameObject.AddComponent<AudioSource>();
            orderAudio.playOnAwake = false; 
            // Setup animator.
            animator = GetComponent<Animator>();
            animator.speed = Random.Range(minAnimSpeed, maxAnimSpeed);
			
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
            AudioManager.instance.PlaySound(drink.audioClipName, ref orderAudio);
			orderText.text = drink.usedIngredient.name;
		}

		int CheckDrink(CompleteDrink drink)
		{
			return recipes.CheckDrink(drink, order);
		}

		void Score(int scoreAmount)
		{
			if(!freePlay)
				manager.AddScore(scoreAmount);
			scoreText = Instantiate(scoreTextObject, textSpawnPosition.position, Quaternion.identity, orderText.transform).GetComponent<TextMeshPro>();
			scoreText.text = scoreAmount.ToString() + " Points";

            // Tell animator to drink and change animation speed.
            animator.SetTrigger("Drink"); 
            animator.speed = Random.Range(minAnimSpeed, maxAnimSpeed);

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

			Destroy(other.gameObject, 0f);
		}
	}
}
