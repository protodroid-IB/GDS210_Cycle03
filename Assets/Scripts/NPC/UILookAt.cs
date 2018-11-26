using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAt : MonoBehaviour
{

    [SerializeField]
    GameObject player;

    Transform playerPos;

	void Start ()
    {

        player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {

            Debug.Log("PC Player not found. Using VR Player. " + player.name);
            player = GameObject.FindGameObjectWithTag("VRPayer");

        }
        else
        {

            Debug.Log("PC Player found! You can use a VR HMD for a more immersive experience! " + player.name);

        }

        playerPos = player.transform;

	}
	
	void Update ()
    {

        transform.LookAt(playerPos);

	}
}
