using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serving;

public class ServingAudio : MonoBehaviour
{
    private AudioSource[] audioSources;

    [SerializeField]
    private bool canPour = false;

    private Pouring pouring;



    private void Awake()
    {
        audioSources = GetComponents<AudioSource>();
    }

    // Use this for initialization
    void Start ()
    {
        if (canPour)
        {
            pouring = GetComponentInChildren<Pouring>();
            audioSources[1].volume = 0f;
            AudioManager.instance.PlaySound("BarGame_Pour", ref audioSources[1], 1f, 0f);
        }
        else pouring = null;
    }

    //POUR SOUND
    void Update()
    {
        if(canPour)
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
