using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThrowingGameController : MonoBehaviour
{
   
    public int score;
    public float timeLimit;
    public float timeRemaining;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timerText;

    [SerializeField] GameObject knivesContainer;
    [SerializeField] GameObject axesContainer;

    GameObject activeKnivesContainer;
    GameObject activeAxesContainer;

    // Reference to the score records container.
    [SerializeField] ScoreRecords throwingGameScores;


    public bool freeplayMode;

	void Start ()
    {
        freeplayMode = false;
        timerText.enabled = false;
    }

	void Update ()
    {
        timerText.text = "Time Remaining: " + timeRemaining.ToString("00.00");

        if (timeRemaining > 0 && !freeplayMode)
        {
            timeRemaining -= Time.deltaTime;
        }

        if (timeRemaining <= 0 && !freeplayMode)
        {
            timeRemaining = 0;
            freeplayMode = true;
            timerText.enabled = false;
            UpdateHighScore();
        }
    }

    public void AddScore(int points)
    {
        if (!freeplayMode)
        {
            score += points;
            scoreText.text = "Score: " + score.ToString("0000");
            throwingGameScores.currentScore = score;    // Reference to score records.
        }
    }

    public void RestartMiniGame(string minigame)
    {
        GameManager.gameManager.RestartMiniGame(minigame);  // Resets the whole scene.
    }

    public void StartFreeplay()
    {
        freeplayMode = true;
        StartThrowingGame(freeplayMode);
    }

    public void StartTimedMode()
    {
        freeplayMode = false;
        StartThrowingGame(freeplayMode);
        timerText.enabled = true;
    }

    void StartThrowingGame(bool freeplaymode)
    {
        if (activeKnivesContainer != null)
            Destroy(activeKnivesContainer);

        if (activeAxesContainer != null)
            Destroy(activeAxesContainer);

        activeKnivesContainer = Instantiate(knivesContainer);
        activeAxesContainer = Instantiate(axesContainer);

        if (!freeplayMode)
        {
            timeRemaining = timeLimit;
        }
    }

    // Updates the throwing games high score.
    void UpdateHighScore()
    {
        GameManager.gameManager.UpdateHighScore(throwingGameScores);
    }
}
