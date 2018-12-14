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

    AudioSource audioSource;

    private int maxChance = 9;

    public bool freeplayMode;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start ()
    {
        freeplayMode = false;
        timerText.enabled = false;
    }

	void Update ()
    {
		int minutes = (int)(timeRemaining / 60);
		int seconds = (int)(timeRemaining - (minutes * 60));
        timerText.text = "Time Remaining: " + minutes.ToString() + ":" + seconds.ToString();

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

    public void AddScore(int points, Collider collider)
    {
        if (!freeplayMode)
        {
            if (collider.gameObject.name == "SwingingTarget")
            {
                points = points * 10;

                maxChance -= 1;

                if(Random.Range(0,maxChance) == 0)
                {
                    AudioManager.instance.PlaySound("ThrowingGame_NiceThrow", ref audioSource);
                    maxChance = 9;
                }
                    
            }   

            score += points;
            UpdateScore();
            throwingGameScores.currentScore = score;    // Reference to score records.
        }
    }

    public void RestartMiniGame()
    {
        GameManager.instance.RestartMiniGame("_ThrowingGame");  // Resets the whole scene.
    }

    public void StartFreeplay()
    {
        freeplayMode = true;
        StartThrowingGame();
    }

    public void StartTimedMode()
    {
        freeplayMode = false;
        StartThrowingGame();
    }

    void StartThrowingGame()
    {
		score = 0;
        UpdateScore();

        if (activeKnivesContainer != null)
            Destroy(activeKnivesContainer);

        if (activeAxesContainer != null)
            Destroy(activeAxesContainer);

        timerText.enabled = !freeplayMode;

        activeKnivesContainer = Instantiate(knivesContainer);
        activeAxesContainer = Instantiate(axesContainer);

        if (!freeplayMode)
        {
            timeRemaining = timeLimit;
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString("0000");
    }

    // Updates the throwing games high score.
    void UpdateHighScore()
    {
        GameManager.instance.UpdateHighScore(throwingGameScores);
    }
}
