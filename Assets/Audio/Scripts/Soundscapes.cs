using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundscapes : MonoBehaviour
{

    [SerializeField] private AudioSource Outside1AS, Outside2AS, InsideAS;

	// Use this for initialization
	void Start ()
    {
        AudioManager.instance.PlaySound("Soundscape_Outside", Outside1AS);
        AudioManager.instance.PlaySound("Soundscape_Outside", Outside2AS);
        AudioManager.instance.PlaySound("Soundscape_Inside", InsideAS);
    }

}
