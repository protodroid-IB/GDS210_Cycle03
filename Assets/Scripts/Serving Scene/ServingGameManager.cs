using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Serving
{
	public class ServingGameManager : MonoBehaviour {

		public static int score = 0;

		[SerializeField]
		ScoreRecords barScoreRecord;

		[SerializeField]
		Button[] gameButtons;

		Order[] customers;

		int shortTime = 30, mediumTime = 60, longTime = 120;

		float timer = 0, gameTime = 30;

		[SerializeField]
		TextMeshPro timerText, scoreText, highText;

		bool game, countdown;

		private void Start()
		{
			highText.text = "Highscore = " + barScoreRecord.highestScore.ToString() + " points";
			scoreText.text = "Score = 0 points";
		}

		private void Update()
		{
			if(countdown)
			{
				timer -= Time.deltaTime;
			}

			if (game)
			{
				timer += Time.deltaTime;
				if (timer >= gameTime)
				{
					foreach (Button button in gameButtons)
					{
						button.enabled = false;
					}

					timer = 0;
					game = false;
					foreach (Order cust in customers)
					{
						cust.EndGame();
					}

                    GameManager.gameManager.UpdateHighScore(barScoreRecord);
                }
			}
			SetTimerText();
		}

		public void AddScore(int scoreToAdd)
		{
			score += scoreToAdd;
			barScoreRecord.currentScore = score;
			scoreText.text = "score = " + score.ToString() + " points";

			if (barScoreRecord.highestScore < score)
			{
				barScoreRecord.highestScore = score;
				highText.text = "Highscore = " + score.ToString() + " points";
			}
		}

		void ResetScore()
		{
			score = 0;
			barScoreRecord.currentScore = score;
			scoreText.text = "score = " + score.ToString() + " points";
		}

		IEnumerator StartGame(int roundTime = 30)
		{
			foreach(Button button in gameButtons)
			{
				button.enabled = false;
			}

			game = true;
			countdown = true;
			timer = 3;
			ResetScore();
			yield return new WaitForSeconds(3);
			timer = 0;
			countdown = false;
			customers = FindObjectsOfType<Order>();
			gameTime = roundTime;
			foreach(Order cust in customers)
			{
				cust.StartGame();
			}

		}

		public void StartShortRound()
		{
			if (game)
				return;
			StartCoroutine(StartGame(shortTime));
		}

		public void StartMediumRound()
		{
			if (game)
				return;
			StartCoroutine(StartGame(mediumTime));
		}

		public void StartLongGame()
		{
			if (game)
				return;
			StartCoroutine(StartGame(longTime));
		}

		void SetTimerText()
		{
			timerText.text = ((int)timer).ToString() + " Left";
		}
	}
}
