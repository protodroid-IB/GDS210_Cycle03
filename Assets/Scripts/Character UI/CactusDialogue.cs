using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CactusDialogue : MonoBehaviour
{

    Animator cactusAnimator;

    [SerializeField]
    GameObject cactus;

    [SerializeField]
    GameObject dialogueWheel;

    [SerializeField]
    GameObject subtitles;

    [SerializeField]
    TextMeshProUGUI dialogueButton;

    [SerializeField]
    TextMeshProUGUI subtitlesText;

    [SerializeField]
    bool isTalking;

	void Start ()
    {

        isTalking = false;
        cactusAnimator = cactus.GetComponent<Animator>();

	}
	

	void Update ()
    {

        DialogueManager();

	}

    public void DialogueManager()
    {

        if(isTalking == false)
        {

            dialogueWheel.SetActive(false);
            dialogueButton.text = "Hi!";

        }else if(isTalking == true)
        {

            dialogueWheel.SetActive(true);
            dialogueButton.text = "Bye!";

        }

    }

    public void Greet()
    {

        //play audio or something
        subtitles.SetActive(true);
        subtitlesText.text = "Howdy!";
        Invoke("subsOff",10.0f);

    }

    public void Antagonise()
    {

        //play audio or something
        subtitles.SetActive(true);
        subtitlesText.text = "That's not very yee haww of ya!";
        Invoke("subsOff", 10.0f);

    }

    public void Tutorial()
    {

        subtitles.SetActive(true);
        subtitlesText.text = "We got shootin', throwin', and servin'.";
        Invoke("Shooting", 5.0f);

    }

    void Shooting()
    {

        subtitlesText.text = "You can head out back o' the ol' saloon. There's a set o' targets you can shoot at, as well as bottles and empty cans.";
        Invoke("Throwing", 15.0f);

    }

    void Throwing()
    {

        subtitlesText.text = "Just by the stairs inside the saloon is an armament of knifes and axes. See if you can stick one on the swingin' target.";
        Invoke("Serving", 15.0f);

    }

    void Serving()
    {

        subtitlesText.text = "Now if you wanna earn a little cash honestly, the bar keep will have you serve drinks. Hope you can keep up!";
        Invoke("subsOff", 15.0f);

    }

    public void Dialogue()
    {

        //play audio or something
        subtitlesText.text = "Just a silly ol' cactus!";
        Invoke("subsOff", 5.0f);

    }

    void subsOff()
    {

        subtitles.SetActive(false);

    }

}
