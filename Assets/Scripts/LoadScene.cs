using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour {
    [SerializeField] string loadSceneName;

    bool loadingScene = false;

    // When the player enters the trigger, the scene will be added to the hierarchy.
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MainCamera" && !loadingScene)
        {
            SceneManagement.sceneManagement.LoadScene(loadSceneName);
            loadingScene = true;
        }
    }
}
