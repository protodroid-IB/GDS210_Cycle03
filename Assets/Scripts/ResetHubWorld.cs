using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VRTK 
{
    public class ResetHubWorld : MonoBehaviour {
        [SerializeField]
        string scene;
        bool loadingScene = false;

        public void ResetHub(DestinationMarkerEventArgs e) {

            print("going back to hubworld");
            //   SceneManager.LoadScene(scene);
            //   SceneManager.LoadScene(scene);
            SteamVR_LoadLevel.Begin(scene, false, 0.5f);
            loadingScene = true;
            // SceneManagement.sceneManagement.ActivateHubworldInstance();

        }
    }
}
