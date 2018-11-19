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

    public int iD;

    private void Start()
    {

        gunScript = gunObject.GetComponent<Gun>();

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Bullet" && bullet.activeInHierarchy == false)
        {

            isLoaded = true;
            bullet.SetActive(true);
            gunScript.bullets++;
            Debug.Log("Rounds: " + gunScript.bullets);

        }

    }

}
