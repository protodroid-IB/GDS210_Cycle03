using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamber : MonoBehaviour
{

    Gun gunScript;

    public bool isLoaded;

    //public GameObject gunCylinder;
    public GameObject gunObject;
    public GameObject bullet;

    Animator animator;

    public int iD;

    private void Start()
    {
        animator = GetComponent<Animator>();
        gunScript = gunObject.GetComponent<Gun>();

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Bullet" && bullet.activeInHierarchy == false)
        {

            isLoaded = true;
            animator.SetTrigger("Load");
            bullet.SetActive(true);
            gunScript.bullets++;
            Debug.Log("Rounds: " + gunScript.bullets);

        }

    }

}
