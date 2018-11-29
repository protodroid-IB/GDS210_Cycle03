using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAt : MonoBehaviour
{

    GameObject player;

    void Start ()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {

            player = GameObject.FindGameObjectWithTag("VRPlayer");

        }

	}
	
	void Update ()
    {

        transform.LookAt(player.transform.position);

	}

}
