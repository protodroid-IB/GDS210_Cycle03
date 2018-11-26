using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DidIHitTarget : MonoBehaviour
{
    ThrowingGameController tgc;

	void Awake ()
    {
        tgc = GameObject.Find("ThrowingGameController").GetComponent<ThrowingGameController>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ThrowingTarget")
            tgc.AddScore(10);
    }
}
