using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundscapes : MonoBehaviour
{

    [SerializeField] private AudioSource[] outsideAudioSources;
    [SerializeField] private AudioSource insideAudioSource;

	// Use this for initialization
	void Start ()
    {
        for(int i=0; i < outsideAudioSources.Length; i++)
        {
            AudioManager.instance.PlaySound("Soundscape_Outside", ref outsideAudioSources[i]);
        }
        
        AudioManager.instance.PlaySound("Soundscape_Inside", ref insideAudioSource);
    }
}
