﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Serving
{
	public class ServingGameManager : MonoBehaviour {

		public static int score = 0;

		[SerializeField]
		ScoreRecords barScoreRecord;

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
					timer = 0;
					game = false;
					foreach (Order cust in customers)
					{
						cust.EndGame();
					}
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

		IEnumerator StartGame(int roundTime = 30)
		{
			game = true;
			countdown = true;
			timer = 3;
			yield return new WaitForSeconds(3);
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
