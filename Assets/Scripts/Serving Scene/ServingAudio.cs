using System.Collections;
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
    private void OnTriggerEnter(Collider collider)
    {
		BottleClink(collider.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
		BottleClink(collision.gameObject);
    }

	void BottleClink(GameObject collider)
	{
		if (!collider.layer.Equals(2))
		{
			if (GetComponent<Rigidbody>().velocity.sqrMagnitude >= 0.5)
			{
				AudioManager.instance.PlaySound("BarGame_Clink", ref audioSources[0]);
			}
		}
	}
}
