using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThrowingGameController : MonoBehaviour
{
    GameManager gameManager;
    
    public int score;
    public float timeLimit;
    public float timeRemaining;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timerText;

    [SerializeField] GameObject knivesContainer;
    [SerializeField] GameObject axesContainer;

    GameObject activeKnivesContainer;
    GameObject activeAxesContainer;


    public bool freeplayMode;

	void Start ()
    {
        gameManager = FindObjectOfType<GameManager>();

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
        }
    }

    public void AddScore(int points)
    {
        if (!freeplayMode)
        {
            score += points;
            scoreText.text = "Score: " + score.ToString("0000");
        }
    }

    public void RestartMiniGame(string minigame)
    {
        gameManager.RestartMiniGame(minigame);
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
}
