using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DidIHitTarget : MonoBehaviour
{
    ThrowingGameController tgc;

    bool used = false;

	void Awake ()
    {
        tgc = GameObject.Find("ThrowingGameController").GetComponent<ThrowingGameController>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ThrowingTarget" && !AmIUsed())
        {
            SetUsed();
            tgc.AddScore(10);
        }
    }

    public void SetUsed()
    {
        used = true;
    }

    public bool AmIUsed()
    {
        if (used)
            return true;
        else
            return false;
    }
}
