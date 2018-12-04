using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseAudio : MonoBehaviour
{
    // 0 = neigh
    // 1 = gallop
    private AudioSource[] audioSources;

    private AudioSource horseGallopAudio;
    private AudioSource horseNeighAudio;

    private Animator animator; 

    [SerializeField]
    private bool isGalloping = false;

    private float minNeighTime = 15f;
    private float maxNeighTime = 60f;

    private float neighTime;
    private float neighTimer = 0f;

    // the amount of semitones the pitch may vary from the original.
    // if the origanl was a D and min and max were -2 and 2 respectively the range would be
    // bwetween C and E
    private float minSemitone = -2f;
    private float maxSemitone = 2f;
    private float newPitch = 1f;

    private bool walking = false;

    private const float PITCH_MULT = 1.05946f;

    // Use this for initialization
    void Start ()
    {
        audioSources = GetComponents<AudioSource>();
        horseNeighAudio = audioSources[0];
        horseGallopAudio = audioSources[1];

        animator = GetComponent<Animator>();

        newPitch = Mathf.Pow(PITCH_MULT, Random.Range(minSemitone, maxSemitone));
        neighTime = Random.Range(minNeighTime, maxNeighTime);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (neighTimer >= neighTime)
        {
            newPitch = Mathf.Pow(PITCH_MULT, Random.Range(minSemitone, maxSemitone));
            neighTime = Random.Range(minNeighTime, maxNeighTime);
            neighTimer = 0f;


            Debug.Log(animator.GetBool("walking"));

            
            AudioManager.instance.PlaySound("HorseNeigh", ref horseNeighAudio, newPitch, 1f);
            
        }
        else
            neighTimer += Time.deltaTime;

        GallopSound();
    }



    private void GallopSound()
    {
        if(isGalloping)
        {
            if (animator.GetBool("walking"))
            {
                if (walking == false)
                {
                    AudioManager.instance.PlaySound("HorseGallop", ref horseGallopAudio);
                    walking = true;
                }
            }
            else
            {
                horseGallopAudio.Stop();
                walking = false;
            }
        }
        
    }




}
