using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetHubWorld : MonoBehaviour {
    [SerializeField] string scene;
    bool loadingScene = false;

    void OnTriggerEnter(Collider col) 
    {

    if (col.tag == "Player" && loadingScene == false) 
        {

        GameManager.spawnPosition = col.transform.position;
        GameManager.spawnPosition.y = 0;

        SceneManager.LoadScene(1, LoadSceneMode.Single);

        loadingScene = true;

        }
    }
 }

