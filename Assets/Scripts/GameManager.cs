using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager gameManager;

    // Players spawn position
    public static Vector3 spawnPosition = Vector3.zero;

    public static bool minigameRestarting = false;

    SceneManagement sceneManagement;

    ScoreManager scoreManager;

    [SerializeField] GameSettings gameSettings;

    [SerializeField] Transform player;

    void Awake () {
        if (gameManager != null)
        {
            Destroy(gameObject);
            return;
        }

        LoadPlayerPrefs();
        sceneManagement = GetComponent<SceneManagement>();
    }

    public void UpdateHighScore(ScoreRecords game)
    {
        scoreManager.UpdateHighScore(game);
    } 

    // Load the game options from Player Prefs into the GameSettings SObject.
    private void LoadPlayerPrefs()
    {
        gameSettings.soundVolume = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
        gameSettings.musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);

        gameSettings.tutorial = PlayerPrefs.GetInt("Tutorial", 1) == 1 ? true : false;
    }

    public void RestartMiniGame(string minigame)
    {
        sceneManagement.RestartMiniGame(minigame);
    }
}
