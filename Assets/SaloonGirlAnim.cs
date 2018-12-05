using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaloonGirlAnim : MonoBehaviour
{

    private Animator anim;

    [SerializeField]
    private float minWaveTime = 15f, maxWaveTime = 30f;

    private float waveTime;
    private float waveTimer = 0f;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();

        Reset();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (waveTimer >= waveTime)
        {
            Reset();
            anim.SetTrigger("wave");
        }
        else
            waveTimer += Time.deltaTime;
	}

    private void Reset()
    {
        waveTimer = 0f;
        waveTime = Random.Range(minWaveTime, maxWaveTime);
    }
}
