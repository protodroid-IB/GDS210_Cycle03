using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour 
{
    [SerializeField] Transform cameraRigTransform;
    [SerializeField] GameObject holsters;

    private void Update() 
    {
        holsters.transform.position = cameraRigTransform.position;

        Quaternion quart = Quaternion.identity;

        quart.eulerAngles = new Vector3(0, cameraRigTransform.rotation.eulerAngles.y, 0);

        holsters.transform.rotation = quart;
    }
}
