using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a game controller script for the Bar Serving minigame.
/// 
/// I had to make this script as a quick way to get some button functions for the menu.
/// Please feel free to utilise this script for other functions if necessary.
/// </summary>

public class BarGameController : MonoBehaviour {
    GameManager gameManager;


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    public void RestartMiniGame(string minigame)
    {
        gameManager.RestartMiniGame(minigame);
    }


}
