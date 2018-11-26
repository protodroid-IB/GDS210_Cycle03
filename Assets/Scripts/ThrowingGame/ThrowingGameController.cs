using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThrowingGameController : MonoBehaviour
{
    public int score;

    public TMP_Text scoreText;

    public void Addscore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString("0000");
    }
}
