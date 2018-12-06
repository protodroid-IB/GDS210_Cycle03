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
    GameObject hiButton;

    [SerializeField]
    GameObject byeButton;

    [SerializeField]
    GameObject subtitles;

    [SerializeField]
    TextMeshProUGUI dialogueButton;

    [SerializeField]
    TextMeshProUGUI subtitlesText;

    private AudioSource thisAudio;
    

	void Start ()
    {
        thisAudio = GetComponent<AudioSource>();
        cactusAnimator = cactus.GetComponent<Animator>();
        dialogueWheel.SetActive(false);
        byeButton.SetActive(false);
        hiButton.SetActive(true);

	}
	

	void Update ()
    {

        if (Input.GetKeyDown("k"))
        {

            Invoke("Tutorial", 0.5f);

        }

	}

    public void Hi()
    {
        dialogueWheel.SetActive(true);
        hiButton.SetActive(false);

    }

    public void Bye()
    {
        AudioManager.instance.PlaySound("Cactus_ByeNow", ref thisAudio);
        dialogueWheel.SetActive(false);
        byeButton.SetActive(true);

    }

    public void Greet()
    {

        AudioManager.instance.PlaySound("Cactus_Howdy", ref thisAudio);
        subtitles.SetActive(true);
        subtitlesText.text = "Howdy!";
        Invoke("subsOff",10.0f);

    }

    public void Antagonise()
    {

        AudioManager.instance.PlaySound("Cactus_NotVeryYeeHaw", ref thisAudio);
        cactusAnimator.SetTrigger("Disappointed");
        subtitles.SetActive(true);
        subtitlesText.text = "That's not very yee haww of ya!";
        Invoke("subsOff", 5.0f);

    }

    public void Tutorial()
    {
        AudioManager.instance.PlaySound("Cactus_ShootingThrowingServing", ref thisAudio);
        subtitles.SetActive(true);
        subtitlesText.text = "We got shootin', throwin', and servin'.";
        Invoke("Shooting", 5.0f);

    }

    void Shooting()
    {
        AudioManager.instance.PlaySound("Cactus_Shooting", ref thisAudio);
        subtitlesText.text = "You can head out back the ol' saloon. There's a set o' targets you can shoot at, as well as bottles and empty cans.";
        Invoke("Throwing", 7.0f);

    }

    void Throwing()
    {
        AudioManager.instance.PlaySound("Cactus_Throwing", ref thisAudio);
        cactusAnimator.SetTrigger("Disagree");
        subtitlesText.text = "Just by the stairs inside the saloon is an armament of knifes and axes. See if you can stick one on the swingin' target.";
        Invoke("Serving", 7.0f);

    }

    void Serving()
    {
        AudioManager.instance.PlaySound("Cactus_Serving", ref thisAudio);
        cactusAnimator.SetTrigger("Agree");
        subtitlesText.text = "Now if you wanna earn a little cash honestly, the bar keep will have you serve drinks. Hope you can keep up!";
        Invoke("subsOff", 7.0f);

    }

    public void Dialogue()
    {
        AudioManager.instance.PlaySound("Cactus_Silly", ref thisAudio);
        cactusAnimator.SetTrigger("Agree");
        subtitles.SetActive(true);
        subtitlesText.text = "Just a silly ol' cactus!";
        Invoke("subsOff", 5.0f);

    }

    void subsOff()
    {

        subtitles.SetActive(false);
        cactusAnimator.SetTrigger("Idle");

    }

}
