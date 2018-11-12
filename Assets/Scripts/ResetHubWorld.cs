using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class ResetHubWorld : MonoBehaviour {
        [SerializeField] string scene;
        bool loadingScene = false;

        void OnTriggerEnter(Collider col) 
        {

        if (col.tag == "MainCamera") 
            {
            print("going back to hubworld");

            GameManager.spawnPosition = col.transform.position;
            GameManager.spawnPosition.y = 0;

            SteamVR_LoadLevel.Begin(scene, false, 0.5f);
            loadingScene = true;
            }
        }
 }

