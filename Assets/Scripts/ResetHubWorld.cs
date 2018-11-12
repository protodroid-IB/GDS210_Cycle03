using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 public class ResetHubWorld : MonoBehaviour {
        [SerializeField] string scene;
        bool loadingScene = false;

        void OnTriggerEnter(Collider col) 
        {

        if (col.tag == "MainCamera") 
            {
            print("going back to hubworld");

            SceneManagement.sceneManagement.SetPlayerSpawn(col.transform.position, col.transform.rotation);

            SteamVR_LoadLevel.Begin(scene, false, 0.5f);
            loadingScene = true;
            }
        }
 }

