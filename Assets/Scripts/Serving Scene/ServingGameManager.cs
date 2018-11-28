using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serving
{
	public class ServingGameManager : MonoBehaviour {

		public static int score = 0;

		[SerializeField]
		ScoreRecords barScoreRecord;

		public void AddScore(int scoreToAdd)
		{
			score += scoreToAdd;
			barScoreRecord.currentScore = score;
			if (barScoreRecord.highestScore < score)
			{
				barScoreRecord.highestScore = score;
			}
		}

		void StartGame(int roundTime = 30)
		{
			Order[] customers = FindObjectsOfType<Order>();
			foreach(Order cust in customers)
			{
				cust.StartGame(roundTime);
			}
		}

		public void StartShortRound()
		{
			StartGame(15);
		}

		public void StartMediumRound()
		{
			StartGame();
		}

		public void StartLongGame()
		{
			StartGame(60);
		}
	}
}
