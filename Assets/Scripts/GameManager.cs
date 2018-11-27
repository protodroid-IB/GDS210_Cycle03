using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager gameManager;

    // Players spawn position
    public static Vector3 spawnPosition = Vector3.zero;

    ScoreManager scoreManager;

    [SerializeField] GameSettings gameSettings;


    [SerializeField] GameObject[] tmProTextFeields;
    [SerializeField] Transform player;

    void Awake () {
        if (gameManager != null)
        {
            Destroy(gameObject);
            return;
        }

        LoadPlayerPrefs();
    }

    private void Update()
    {
        foreach(GameObject tmProTextField in tmProTextFeields)
        {
            tmProTextField.transform.LookAt(2 * tmProTextField.transform.position - player.transform.position);
        }
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
}
