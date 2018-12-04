namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class InteractableObject : MonoBehaviour
    {
        void OnColliderEnter(Collision collision)
        {
            if (collision.gameObject.layer == 2)
            {
                InputController inputController = collision.gameObject.GetComponent<InputController>();

                if (inputController == null)
                    inputController = collision.gameObject.GetComponentInChildren<InputController>();

                inputController.AddPotential(gameObject);
            }
        }

        void OnColliderExit(Collision collision)
        {
            if (collision.gameObject.layer == 2)
            {
                InputController inputController = collision.gameObject.GetComponent<InputController>();

                if (inputController == null)
                    inputController = collision.gameObject.GetComponentInParent<InputController>();

                inputController.RemovePotential();
            }
        }
    }
}
