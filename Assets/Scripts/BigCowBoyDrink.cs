using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BigCowBoyDrink : MonoBehaviour {
    Animator myAnimator;

	// Use this for initialization
	void Start () {
        myAnimator = GetComponent<Animator>();
        myAnimator.speed = Random.Range(0.950f, 1.105f);

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.O))
        {
            DrinkUp();
        }

    }

    public void DrinkUp()
    {
        myAnimator.SetTrigger("Drink");
        myAnimator.speed = Random.Range(0.950f, 1.105f);
    }
}
