using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    public float timer;
    
	void Update () {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
