using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CactusDialogue : MonoBehaviour
{

    [SerializeField]
    GameObject dialogueWheel;

    [SerializeField]
    TextMeshPro dialogueButton;

    bool isTalking;

	void Start ()
    {

        isTalking = false;
        dialogueButton = GetComponent<TextMeshPro>();

	}
	

	void Update ()
    {
		


	}

    public void DialogueManager()
    {

        if(isTalking == false)
        {

            dialogueWheel.SetActive(true);
            dialogueButton.text = "Hi!";

        }else if(isTalking == true)
        {

            dialogueWheel.SetActive(false);
            dialogueButton.text = "Bye!";

        }

    }

    public void Greet()
    {

        //play audio or something

    }

    public void Antagonise()
    {

        //play audio or something

    }

    public void Tutorial()
    {

        //play audio or something
        //spawn gun or something

    }

    public void Dialogue()
    {

        //play audio or something

    }

}
