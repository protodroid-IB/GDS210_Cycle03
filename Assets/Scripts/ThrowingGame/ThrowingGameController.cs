using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThrowingGameController : MonoBehaviour
{
    GameManager gameManager;
    public int score;

    [SerializeField] TMP_Text scoreText;

	void Start ()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

	void Update ()
    {
		
	}

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString("0000");
    }

    public void RestartMiniGame(string minigame)
    {
        gameManager.RestartMiniGame(minigame);
    }
}
