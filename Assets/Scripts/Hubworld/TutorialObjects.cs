using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObjects : MonoBehaviour {
    // Place this item on any tutorial object in the game.

    private void Start()
    {
        // Add this item to the list on start.
        GameManager.tutorialObjects.Add(gameObject);

        bool active = GameManager.gameManager.gameSettings.tutorial;
        gameObject.SetActive(active); // Ensures objects on start follow the games settings.
    
    }

    private void OnDestroy()
    {
        // Remove from the list if it is ever destroyed.
        GameManager.tutorialObjects.Remove(gameObject);
    }
}






