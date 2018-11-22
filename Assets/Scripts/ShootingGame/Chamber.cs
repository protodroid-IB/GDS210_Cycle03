using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamber : MonoBehaviour
{
    public float ejectForce;
    public float ejectTorque;

    [HideInInspector] public bool isLoaded = true;
    [HideInInspector] public bool isEjected = false;

    public GameObject cylinder;
    public GameObject bullet;
    public GameObject shell;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" && bullet.activeInHierarchy == false)
        {
            isLoaded = true;
            animator.SetTrigger("Load");
            bullet.SetActive(true);
        }
    }

    public void Eject()
    {
        isEjected = true;
        var rb = Instantiate(shell, bullet.transform.position, bullet.transform.rotation).GetComponent<Rigidbody>();
        rb.AddForce(-transform.up * ejectForce,ForceMode.Impulse);
        rb.AddTorque(new Vector3(Random.Range(0f, ejectTorque), Random.Range(0f, ejectTorque), Random.Range(0f, ejectTorque)));
        bullet.SetActive(false);
    }
}