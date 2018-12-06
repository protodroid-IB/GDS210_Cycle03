using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] GameObject deadBird;

    public void Die()
    {
        Instantiate(deadBird, transform.position, transform.rotation);

        GetComponent<Rigidbody>().angularVelocity = Vector3.forward;

        Destroy(gameObject);
    }
}
