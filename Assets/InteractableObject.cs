using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour 
{
	void OnColliderEnter (Collision collision)
    {
        if (collision.gameobject.layer == 2)
        {
            InputController inputController = collision.GetComponent<InputController>();

            if (inputController == null)
                inputController = collision.GetComponentInChildren();

            inputController.AddPotential();
        }
    }

    void OnColliderExit(Collision collision)
    {
        if (collision.gameobject.layer == 2)
        {
            InputController inputController = collision.GetComponent<InputController>();

            if (inputController == null)
                inputController = collision.GetComponentInParent();

            inputController.RemovePotential();
        }
    }
}
