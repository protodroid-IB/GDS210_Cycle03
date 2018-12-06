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

		int shortTime = 60, mediumTime = 180, longTime = 300;

		float timer = 0, gameTime = 0;

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

		IEnumerator StartGame(int roundTime)
		{
			foreach(Button button in gameButtons)
			{
				button.enabled = false;
			}

			
			countdown = true;
			timer = 3;
			ResetScore();
			yield return new WaitForSeconds(3);
			game = true;
			countdown = false;
			timer = 0;
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
			int timeLeft = (int)(gameTime - timer);
			int minutes = timeLeft / 60;
			int seconds = timeLeft - (minutes * 60);
			timerText.text = minutes.ToString() + ":" + seconds.ToString();
		}
	}
}
