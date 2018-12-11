using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(AudioSource))]
public class CharacterDialogue : MonoBehaviour
{
	private SphereCollider audioCollider;
	private AudioSource dialogueAudio;

	[SerializeField]
	private string shotDialogue;

	[SerializeField]
	private string[] dialogueOptions;

	private int dialogueIndex;





	private bool speak = false;

	[SerializeField]
	private float minTime = 10f, maxTime = 20f;

	private float speakTime;
	private float speakTimer = 0f;





	// Use this for initialization
	void Start ()
	{
		audioCollider = GetComponent<SphereCollider>();
		audioCollider.isTrigger = true;

		dialogueAudio = GetComponent<AudioSource>();

		speakTime = Random.Range(minTime, maxTime);
		dialogueIndex = Random.Range(0, dialogueOptions.Length);
	}




	
	// Update is called once per frame
	void Update ()
	{
		if(speak)
		{
			if (speakTimer == speakTime)
			{
				speakTime = 0f;
				dialogueIndex = Random.Range(0, dialogueOptions.Length);
				AudioManager.instance.PlaySound(dialogueOptions[dialogueIndex], ref dialogueAudio);

			}
			else speakTimer += Time.deltaTime;

			Debug.Log("I CAN SPEAK!!!");
		}
	}




	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player" && other.gameObject.layer == 8)
		{
			speak = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player" && other.gameObject.layer == 8)
		{
			speak = false;
		}
	}


	public void PlayShotAudio()
	{
		AudioManager.instance.PlaySound(shotDialogue, ref dialogueAudio);
	}
}
