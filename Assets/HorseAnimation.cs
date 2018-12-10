using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseAnimation : MonoBehaviour {

	Animator animator;

	[SerializeField]
	float newSpeed, currentSpeed;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		StartCoroutine(ChangeSpeed());
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(currentSpeed - newSpeed) >= 0.001f)
		{
			currentSpeed = Mathf.Lerp(currentSpeed, newSpeed, Time.deltaTime);
			animator.SetFloat("Speed", currentSpeed);
		}
	}

	IEnumerator ChangeSpeed()
	{
		yield return new WaitForSeconds(3);
		StartCoroutine(ChangeSpeed());
		newSpeed = Random.Range(0.6f, 1.4f);
	}
}
