using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(AudioSource))]
public class CharacterDialogue : MonoBehaviour
{
	private AudioSource dialogueAudio;

	[SerializeField]
	private string shotDialogue;

	[SerializeField]
	private string[] dialogueOptions;

	private int dialogueIndex;





	public bool speak = false;

	[SerializeField]
	private float minTime = 10f, maxTime = 20f;

	private float speakTime;
	private float speakTimer = 0f;





	// Use this for initialization
	void Start ()
	{

		dialogueAudio = GetComponent<AudioSource>();

		speakTime = Random.Range(minTime, maxTime);
		dialogueIndex = Random.Range(0, dialogueOptions.Length);
	}




	
	// Update is called once per frame
	void Update ()
	{
		if(speak)
		{
			if (speakTimer >= speakTime)
			{
				speakTimer = 0f;
                speakTime = Random.Range(minTime, maxTime);
                if (dialogueOptions.Length >= 1)
                {
                    dialogueIndex = Random.Range(0, dialogueOptions.Length);
                    AudioManager.instance.PlaySound(dialogueOptions[dialogueIndex], ref dialogueAudio);
                }
                speak = false;
			}
			else speakTimer += Time.deltaTime;

			Debug.Log("I CAN SPEAK!!!");
		}
	}


	public void PlayShotAudio()
	{
		AudioManager.instance.PlaySound(shotDialogue, ref dialogueAudio);
	}
}
