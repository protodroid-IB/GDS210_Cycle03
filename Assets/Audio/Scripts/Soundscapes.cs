using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundscapes : MonoBehaviour
{

    public AudioSource[] outsideAudioSources;   // Made public so I could access the functoins in the MenuSettings Class.
    public AudioSource insideAudioSource;

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
