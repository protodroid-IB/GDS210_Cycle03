﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serving;

public class ServingAudio : MonoBehaviour
{
    private AudioSource[] audioSources;

    private Pouring pouring;



    private void Awake()
    {
        audioSources = GetComponents<AudioSource>();
        if(audioSources.Length < 2)
        {
            AudioSource source = (audioSources.Length == 1)? audioSources[0] : gameObject.AddComponent<AudioSource>();
            audioSources = new AudioSource[2];
            audioSources[0] = source;
            audioSources[1] = gameObject.AddComponent<AudioSource>();
        }
    }

    // Use this for initialization
    void Start ()
    {
        pouring = GetComponentInChildren<Pouring>();

        if (pouring)
        {   
            audioSources[1].volume = 0f;
            AudioManager.instance.PlaySound("BarGame_Pour", ref audioSources[1], 1f, 0f);
        }
    }

    //POUR SOUND
    void Update()
    {
        if(pouring)
        {
            audioSources[1].volume = pouring.strength;
        }         
    }




    //CLINK SOUND
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.layer.Equals(2))
            AudioManager.instance.PlaySound("BarGame_Clink", ref audioSources[0]);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.layer.Equals(2))
            AudioManager.instance.PlaySound("BarGame_Clink", ref audioSources[0]);
    }
}
