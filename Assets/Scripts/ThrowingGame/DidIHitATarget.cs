using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DidIHitATarget : MonoBehaviour
{
    ThrowingGameController throwingGameController;

    private void Awake()
    {
        throwingGameController = GameObject.Find("ThrowingGameController").GetComponent<ThrowingGameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ThrowingTarget")
            throwingGameController.Addscore(10);
    }
}
