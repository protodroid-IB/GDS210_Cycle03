using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager gameManager;

    public bool gameStarted = false;

    [SerializeField] PlayerSpawnLocation playerSpawnLocation;

    void Awake () {
        if (gameManager != null)
        {
            Destroy(gameObject);
            return;
        }

        if(gameStarted == false)
        {
            playerSpawnLocation.spawnLocation = Vector3.zero;
            gameStarted = false;
        }
    }
}
