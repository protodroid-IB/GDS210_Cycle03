using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThrowingGameController : MonoBehaviour
{
    public int score;

    [SerializeField] TMP_Text scoreText;

	void Start ()
    {
		
	}

	void Update ()
    {
		
	}

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString("0000");
    }
}
