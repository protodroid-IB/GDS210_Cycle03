using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusDialogue : MonoBehaviour
{

    [SerializeField]
    GameObject dialogueWheel;

    bool isTalking;

	void Start ()
    {

        isTalking = false;

	}
	

	void Update ()
    {
		


	}

    public void DialogueManager()
    {

        if(isTalking == false)
        {

            dialogueWheel.SetActive(true);

        }else if(isTalking == true)
        {

            dialogueWheel.SetActive(false);

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
