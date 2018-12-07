using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObjects : MonoBehaviour {
    [SerializeField] GameSettings gameSettings;
    
    [SerializeField] Transform[] children;
	
	// Update is called once per frame
	void Update () {
        
        print(gameObject.activeSelf);
	}
}
