using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] float forwardForce;
    [SerializeField] float leftForce;
    [SerializeField] float upTorque;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start ()
    {
        rb.AddRelativeForce(Vector3.forward * forwardForce, ForceMode.Impulse);
        rb.AddRelativeForce(Vector3.left * leftForce, ForceMode.Impulse);
        rb.AddRelativeTorque(Vector3.up * upTorque, ForceMode.Impulse);
    }
	
	void Update ()
    {
        
    }
}
