using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAudio : MonoBehaviour
{
    private AudioSource thisAudio;

    private void Start()
    {
        thisAudio = GetComponent<AudioSource>();
    }

	public void PlayPickUpSound()
    {
        AudioManager.instance.PlaySound("PickUpItem", ref thisAudio);
    }
}
