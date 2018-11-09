using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script can be attached to any game object that you wish to remain active between scenes.
/// </summary>

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        // do not destroy this gameobject when a new scene is loaded
        DontDestroyOnLoad(this.gameObject);


        // if more than one of this type of object exists
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            // destroy the gameobject
            Destroy(gameObject);
        }
    }
}
