using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DidIHitTarget : MonoBehaviour
{
    private AudioSource knifeAudio;

    ThrowingGameController tgc;

    bool used = false;

    [SerializeField]
    private LayerMask handsLayer;

	void Awake ()
    {
        tgc = GameObject.Find("ThrowingGameController").GetComponent<ThrowingGameController>();
        knifeAudio = GetComponent<AudioSource>();
	}

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "ThrowingTarget" && !AmIUsed())
        //{
        //    SetUsed();
        //    tgc.AddScore(10, other);
        //}

        if(!other.gameObject.layer.Equals(2))
            AudioManager.instance.PlaySound("ThrowingGame_Hit", ref knifeAudio);

    }

    private void OnCollisionEnter(Collision collision)
    {
        // need to perform a check
        // if not player grabbing in VR
        if (!collision.gameObject.layer.Equals(2))
            AudioManager.instance.PlaySound("ThrowingGame_Hit", ref knifeAudio);
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
