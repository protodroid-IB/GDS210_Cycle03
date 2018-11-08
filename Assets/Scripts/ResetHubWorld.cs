using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetHubWorld : MonoBehaviour {
    [SerializeField] string scene;
    bool loadingScene = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "VRPlayer" && !loadingScene)
        {
            print("going back to hubworld");
         //   SceneManager.LoadScene(scene);
         //   SceneManager.LoadScene(scene);
            SteamVR_LoadLevel.Begin(scene, false, 0.5f);
            loadingScene = true;
            // SceneManagement.sceneManagement.ActivateHubworldInstance();


        }
    }
}
