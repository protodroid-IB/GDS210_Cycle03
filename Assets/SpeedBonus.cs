using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBonus : MonoBehaviour {

    SpriteRenderer renderer;
    Color color = Color.white;
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
        transform.position += Vector3.up * 2 * Time.deltaTime;
        if (renderer.color.a > 0)
        {
            color.a -= 2 * Time.deltaTime;
            renderer.color = color;
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
