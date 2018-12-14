﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public float hammerTension;
    public float hammerSensitivity;
    public float hammerMax;
    public float cylinderSensitivity;
    public float cylinderLerpTime;
    public float addForce;
    
    bool primed;
    bool triggerDown;
    float hammerPull;
    float hammerVal;
    float hammerTarget;
    public GameObject hammer;

    public Chamber[] bullets;
    [HideInInspector] public int bulletIndex = 6;
    GameObject currentBullet;

    [HideInInspector] public bool loaded;
    float cylinderTargetAngle;
    public Cylinder cylinder;
    public float cylinderDrop;

    public Transform firePos;
    public GameObject effect;

    private AudioSource[] gunAudio;
    public GameObject puff;
    public GameObject spark;

    public ShootingGameController sgc;

    [Space(5)]
    [Header("AUDIO STUFF")]

    private const float PITCH_MULT = 1.05946f;

    [SerializeField]
    private float minPingSemitone = -2f;
    [SerializeField]
    private float maxPingSemitone = 2f;


    private void Start()
    {
        gunAudio = GetComponents<AudioSource>(); //0 is shot and hammer, 1 is hit
    }

    private void Update()
    {
        cylinder.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(cylinder.transform.localEulerAngles.z, cylinderTargetAngle, cylinderLerpTime * Time.deltaTime));

        if (!primed && hammerPull == 0f)
        {
            HammerSet(Mathf.Lerp(hammerVal, 0f, hammerTension * Time.deltaTime));
        }

        //checks to see if gun is loaded, also useful for other functions
        if(cylinder.transform.localEulerAngles.z < 1f)
        {
            loaded = true;
            cylinder.cylinderTorque = 0f;
        }
        else if(cylinder.transform.localEulerAngles.z >= 1f)
        {
            loaded = false;
        }
    }

    public void HammerPull(float val)
    {
        hammerPull = val;
        if (!primed)
        {
            if (val > 0f)
            {
                HammerSet(Mathf.MoveTowardsAngle(hammerVal, 1f, hammerPull * hammerSensitivity * Time.deltaTime));
            }
        }
    }

    void HammerSet(float val)
    {
        hammerVal = val;
        float angle;
        if (hammerVal == 1)
        {
            if (primed == false)
            {
                AudioManager.instance.PlaySound("ShootGame_Hammer", ref gunAudio[0]);
            }
            primed = true;
            angle = hammerMax;
        }
        else
        {
            angle = Mathf.LerpAngle(0f, hammerMax, hammerVal);
        }
        hammer.transform.localEulerAngles = new Vector3(angle, 0f, 0f);
    }

    public void TriggerPull(float val)
    {
        if (val > 0f)
        {
            if (!triggerDown)
            {
                if (primed && loaded)
                {
                    SetBullet();
                    if (bullets[bulletIndex].isLoaded)
                    {
                        bullets[bulletIndex].isLoaded = false;
                        bullets[bulletIndex].isEjected = false;
                        Fire();
                    }
                }
                else
                {
                    HammerSet(val);
                }
            }
        }
        else
        {
            triggerDown = false;
        }
    }

    public void Fire()
    {
        AudioManager.instance.PlaySound("ShootGame_ShotFired", ref gunAudio[0]);
        primed = false;
        triggerDown = true;
        HammerSet(0f);
        Instantiate(effect, firePos);
        RaycastHit hit;
        if (sgc)
        {
            sgc.AddShot();
        }
        if (Physics.SphereCast(firePos.transform.position, 0.1f, transform.forward, out hit, 1 << 8))
        {
            var col = hit.collider;
            if (col.CompareTag("Object"))
            {
                var rb = col.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = col.transform.parent.GetComponentInChildren<Rigidbody>();
                }
                if (rb != null)
                {
                    float newPitch = Mathf.Pow(PITCH_MULT, Random.Range(minPingSemitone, maxPingSemitone));
                    AudioManager.instance.PlaySound("ShootGame_Ping", ref gunAudio[1], newPitch);
                    Instantiate(spark, hit.point, Quaternion.identity);
                    rb.AddForce(transform.forward * addForce);
                }

                if (transform.parent != null)
                {
                    var target = col.transform.parent.GetComponentInChildren<Target>();
                    if (target != null)
                    {
                        float newPitch = Mathf.Pow(PITCH_MULT, Random.Range(minPingSemitone, maxPingSemitone));
                        AudioManager.instance.PlaySound("ShootGame_Ting", ref gunAudio[1], newPitch);
                        Instantiate(spark, hit.point, Quaternion.identity);
                        target.AddScore();
                    }
                }

                // If the bird is shot, run it's die function
                if (hit.transform.name == "Bird")
                {
                    hit.transform.gameObject.GetComponent<BirdController>().Die();
                }

				if( hit.transform.GetComponent<CharacterDialogue>())
				{
					hit.transform.GetComponent<CharacterDialogue>().PlayShotAudio();
				}

				// THIS IS SOME DUMB CODE - PLEASE FORGIVE ME SENPAI
				if(hit.transform.GetComponent<FixedJoint>())
				{
					FixedJoint fixedJoint = hit.transform.GetComponent<FixedJoint>();
					if (fixedJoint.connectedBody.name == "Head_end")
					{
						Transform characterTrans = fixedJoint.connectedBody.transform;

						characterTrans.GetComponent<HatDialogueReference>().charDialogue.PlayShotAudio();
					}
				}

            }
            else
            {
                AudioManager.instance.PlaySound("ThrowingGame_Hit", ref gunAudio[1]);
                Instantiate(puff, hit.point, Quaternion.identity);
            }
        }
        //if bullets is equal to current bullet's ID set current bullet inactive take one from bullets and replace current bullet
    }
    
    public void Reload()
    {
        if (loaded == true)
        {
            Eject();
        }
        else if (loaded == false)
        {
            Insert();
        }

        AudioManager.instance.PlaySound("ShootGame_Hammer", ref gunAudio[0]);
    }

    public void Eject()
    {
        cylinderTargetAngle = cylinderDrop;
        cylinder.lastAngle = 0f;
        foreach (Chamber bullet in bullets)
        {
            if (!bullet.isLoaded && !bullet.isEjected)
            {
                bullet.Invoke("Eject", 0.1f);
            }
        }
    }

    public void Insert()
    {
        cylinderTargetAngle = 0f;
        SetBullet();
    }

    public void SetBullet()
    {
        bulletIndex = bulletIndex < bullets.Length -1 ? bulletIndex + 1 : 0;
        currentBullet = bullets[bulletIndex].gameObject;
    }
}