using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundscapes : MonoBehaviour
{

    [SerializeField] private AudioSource Outside1AS, Outside2AS, InsideAS;

	// Use this for initialization
	void Start ()
    {
        AudioManager.instance.PlaySound("Soundscape_Outside", ref Outside1AS);
        AudioManager.instance.PlaySound("Soundscape_Outside", ref Outside2AS);
        AudioManager.instance.PlaySound("Soundscape_Inside", ref InsideAS);
    }


    private void Update()
    {
        if (Outside1AS.isPlaying) Debug.Log("OUTSIDE PLAYING!");
        if (Outside2AS.isPlaying) Debug.Log("OUTSIDE PLAYING!");
        if (InsideAS.isPlaying) Debug.Log("INSIDE PLAYING!");
    }


}
