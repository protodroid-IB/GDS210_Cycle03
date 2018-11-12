using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager gameManager;
    public static Vector3 spawnPosition = Vector3.zero;

    void Awake () {
        if (gameManager != null)
        {
            Destroy(gameObject);
            return;
        }

     //   DontDestroyOnLoad(gameObject);
    }
}
