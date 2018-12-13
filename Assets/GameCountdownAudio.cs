using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameCountdownAudio : MonoBehaviour
{

    public UnityEvent startGameFunction;
    public UnityEvent startFreePlayFunction;
    public UnityEvent retryFunction;

    private AudioSource thisAudio;

    private bool gameStarted = false;
    private bool freePlayStarted = false;
    private bool retry = false;


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


        if(retry)
        {
            if (!thisAudio.isPlaying)
            {
                retryFunction.Invoke();
                retry = false;
            }
        }
	}


    public void StartGame()
    {
        if (gameStarted == false && freePlayStarted == false && retry == false)
        {
            AudioManager.instance.PlaySound("Games_Countdown", ref thisAudio);
            gameStarted = true;
        }
        
    }


    public void StartFreePlay()
    {
        if (gameStarted == false && freePlayStarted == false && retry == false)
        {
            AudioManager.instance.PlaySound("Games_Countdown", ref thisAudio);
            freePlayStarted = true;
        }
    }


    public void RetryGame()
    {
        if (gameStarted == false && freePlayStarted == false && retry == false)
        {
            AudioManager.instance.PlaySound("Games_Countdown", ref thisAudio);
            retry = true;
        }
    }
}
