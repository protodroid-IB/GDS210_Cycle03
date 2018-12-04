using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class IntermittentlyPlaySound : MonoBehaviour
{
    [SerializeField]
    private string soundName;

    [SerializeField]
    private float minPlayTime = 15f, maxPlayTime = 60f;

    private float playTime;
    private float playTimer = 0f;


    [Space(5)]
    [Header("The amount of semitones the pitch may vary from the original. If the originl was a D and min and max were -2 and 2 respectively the range would be bwetween C and E")]
    [SerializeField]
    private float minSemitone = -2f;
    [SerializeField]
    private float maxSemitone = 2f;

    private float newPitch = 1f;

    private const float PITCH_MULT = 1.05946f;

    private AudioSource thisAudio;

    // Use this for initialization
    void Start ()
    {
        thisAudio = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playTimer >= playTime)
        {
            newPitch = Mathf.Pow(PITCH_MULT, Random.Range(minSemitone, maxSemitone));
            playTime = Random.Range(minPlayTime, maxPlayTime);
            playTimer = 0f;

            AudioManager.instance.PlaySound(soundName, ref thisAudio, newPitch);
        }
        else
            playTimer += Time.deltaTime;
    }
}
