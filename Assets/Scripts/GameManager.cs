using UnityEngine;

[RequireComponent(typeof(SceneManagement), typeof(ScoreManager))]
public class GameManager : MonoBehaviour {
    public static GameManager gameManager;

    // Players spawn position
    public static Vector3 spawnPosition = Vector3.zero;

    public static bool minigameRestarting = false;

    SceneManagement sceneManagement;

    ScoreManager scoreManager;

    [SerializeField] GameSettings gameSettings;

    [SerializeField] Transform player;

    [SerializeField] Poster[] wantedPosters;

    void Awake () {
        if (gameManager != null)
        {
            Destroy(gameObject);
            return;
        }

        gameManager = this;

        sceneManagement = GetComponent<SceneManagement>();
        scoreManager = GetComponent<ScoreManager>();

        // Load the games saved data.
        LoadPlayerPrefs();
        scoreManager.LoadScores();
    }

    // Updates the high score on the scriptable object.
    public void UpdateHighScore(ScoreRecords game)
    {
        if(game.currentScore > game.highestScore)
        {
            print("Updating highScore");
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
}
