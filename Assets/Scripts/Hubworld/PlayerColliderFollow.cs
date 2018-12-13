using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderFollow : MonoBehaviour {
    [SerializeField] GameObject playerCollider;

    List<CharacterDialogue> objects = new List<CharacterDialogue>();
    [SerializeField]
    float distance;

    [SerializeField]
    LayerMask layer;

    void Update()
    {
        playerCollider.transform.position = transform.position;

        Collider[] colliders = Physics.OverlapSphere(transform.position, distance, layer);
        foreach (Collider col in colliders)
        {
            CharacterDialogue dialogue = col.GetComponent<CharacterDialogue>();
            if(dialogue)
                dialogue.speak = true;
        }
    }

}
