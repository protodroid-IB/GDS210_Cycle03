using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SceneManagement), typeof(ScoreManager))]
public class GameManager : MonoBehaviour {
    public static GameManager instance;

    // Players spawn position
    public static Vector3 spawnPosition = Vector3.zero;

    public static bool minigameRestarting = false;

    SceneManagement sceneManagement;

    ScoreManager scoreManager;

    public GameSettings gameSettings;

    [SerializeField] Transform player;

    [SerializeField] Poster[] wantedPosters;

    private AudioSource[] highscoreAudio;

    // List of all tutorial objects in the game. Items are added to the list when they are spawned through the Tutorial Objects script.
    public static List<GameObject> tutorialObjects; // = new List<GameObject>();


    void Awake () {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        sceneManagement = GetComponent<SceneManagement>();
        scoreManager = GetComponent<ScoreManager>();

        // Load the games saved data.
        LoadPlayerPrefs();
        scoreManager.LoadScores();

        tutorialObjects = new List<GameObject>();

    }

    private void Start()
    {
        highscoreAudio = new AudioSource[2];

        highscoreAudio[0] = gameObject.AddComponent<AudioSource>();
        highscoreAudio[1] = gameObject.AddComponent<AudioSource>();

        SetTutorialActive();
    }

    // Updates the high score on the scriptable object.
    public void UpdateHighScore(ScoreRecords game)
    {
        if(game.currentScore > game.highestScore)
        {
            AudioManager.instance.PlaySound("Voice_Highscore", ref highscoreAudio[0]);
            AudioManager.instance.PlaySound("Highscore", ref highscoreAudio[1], 1f, 0.75f);

            game.highestScore = game.currentScore;
            scoreManager.SaveScores();

            UpdatePosters();
        }
    }

    // Update the Posters with new highscore.
    public void UpdatePosters()
    {
        foreach (Poster post in wantedPosters)
        {
            post.UpdateScore();
        }
    }

    // Load the game options from Player Prefs into the GameSettings SObject.
    private void LoadPlayerPrefs()
    {
        gameSettings.soundVolume = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
        gameSettings.musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);

        gameSettings.tutorial = PlayerPrefs.GetInt("Tutorial", 1) == 1 ? true : false;
    }

    // Resets the whole game.
    public void RestartMiniGame(string minigame)
    {
        sceneManagement.RestartMiniGame(minigame);
    }

    public void SetTutorialActive()
    {
        bool toggle = gameSettings.tutorial;
        foreach (GameObject tute in tutorialObjects)
        {
            if(tute != null)
                tute.SetActive(toggle);
        }
    }
}
