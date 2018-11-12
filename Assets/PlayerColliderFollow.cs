using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderFollow : MonoBehaviour {
    [SerializeField] GameObject playerCollider;

	void Update () {
        playerCollider.transform.position = transform.position;
	}
}
