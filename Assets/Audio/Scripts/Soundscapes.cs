using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundscapes : MonoBehaviour
{

    [SerializeField] private AudioSource Outside1AS, Outside2AS, Outside3AS, InsideAS;

	// Use this for initialization
	void Start ()
    {
        AudioManager.instance.PlaySound("Soundscape_Outside", ref Outside1AS);
        AudioManager.instance.PlaySound("Soundscape_Outside", ref Outside2AS);
        AudioManager.instance.PlaySound("Soundscape_Outside", ref Outside3AS);
        AudioManager.instance.PlaySound("Soundscape_Inside", ref InsideAS);
    }
}
