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

		int roundLength = 180;

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
					StopGame();
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
				// barScoreRecord.highestScore = score;
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

			StopGame();
			
			countdown = true;
			timer = 3;
			ResetScore();

			yield return new WaitForSeconds(3);
			game = true;
			countdown = false;
			timer = 0;
			gameTime = roundTime;

			customers = FindObjectsOfType<Order>();

			ManageCustomers(true, false);

		}

		public void StopGame()
		{
			if (!game)
				return;

			ManageCustomers(false, false);

			timer = 0;
			game = false;

			GameManager.gameManager.UpdateHighScore(barScoreRecord);
		}

		public void StartRound()
		{
			if (game)
				return;
			StartCoroutine(StartGame(roundLength));
		}

		public void StartFreePlay()
		{
			if (game)
				return;

			ManageCustomers(true, true);
		}

		void ManageCustomers(bool start, bool free)
		{
			customers = FindObjectsOfType<Order>();
			foreach (Order cust in customers)
			{
				if (start)
					cust.StartGame();
				else
					cust.EndGame();

				cust.freePlay = free;
			}
		}

		void SetTimerText()
		{
            int timeLeft = (int)(gameTime - timer);
            if (countdown)
            {
                timeLeft = (int)timer;
            }
			
			int minutes = timeLeft / 60;
			int seconds = timeLeft - (minutes * 60);
			timerText.text = minutes.ToString() + ":" + seconds.ToString();
		}
	}
}
