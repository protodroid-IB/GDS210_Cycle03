using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RisingScoreText : MonoBehaviour {


	TextMeshPro text;

	private void Start()
	{
		Destroy(gameObject, 5);
		text = GetComponent<TextMeshPro>();
	}

	private void Update()
	{
		transform.Translate(Vector3.up * Time.deltaTime);
		Color textColor = text.color;
		textColor.a -= (Time.deltaTime / 5);
	}
}
