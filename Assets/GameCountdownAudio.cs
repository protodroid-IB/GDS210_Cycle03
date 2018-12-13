using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameCountdownAudio : MonoBehaviour
{

    public UnityEvent startGameFunction;
    public UnityEvent startFreePlayFunction;

    private AudioSource thisAudio;

    private bool gameStarted = false;
    private bool freePlayStarted = false;


	// Use this for initialization
	void Start ()
    {
        thisAudio = gameObject.AddComponent<AudioSource>();

       
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(gameStarted)
        {
            if (!thisAudio.isPlaying)
            {
                startGameFunction.Invoke();
                gameStarted = false;
            }
        }

        if(freePlayStarted)
        {
            if (!thisAudio.isPlaying)
            {
                startFreePlayFunction.Invoke();
                freePlayStarted = false;
            }
        }
	}


    public void StartGame()
    {
        if(gameStarted == false && freePlayStarted == false)
        {
            AudioManager.instance.PlaySound("Games_Countdown", ref thisAudio);
            gameStarted = true;
        }
        
    }


    public void StartFreePlay()
    {
        if (gameStarted == false && freePlayStarted == false)
        {
            AudioManager.instance.PlaySound("Games_Countdown", ref thisAudio);
            freePlayStarted = true;
        }
    }
}
