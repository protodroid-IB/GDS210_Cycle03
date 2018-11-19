using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public float hammerTension;
    public float hammerSensitivity;
    public float hammerMax;
    public float addForce;

    [SerializeField]
    public bool loaded;

    bool primed;
    bool triggerDown;
    float hammerPull;
    float hammerVal;
    float hammerTarget;

    public int bullets = 6;

    public GameObject hammer;
    public Cylinder cylinder;
    public GameObject currentBullet;

    public Transform firePos;

    public GameObject effect;

    public GameObject[] cylinderBullets;

    private void Update()
    {
        if (!primed && hammerPull == 0f)
        {
            HammerSet(Mathf.Lerp(hammerVal, 0f, hammerTension * Time.deltaTime));
        }

        //checks to see if gun is loaded, also useful for other functions
        if(cylinder.transform.localEulerAngles == Vector3.zero)
        {

            loaded = true;

        }
        else if(cylinder.transform.localEulerAngles != Vector3.zero)
        {
            loaded = false;
        }

        //should iterate through each bullet to find bullet id
        foreach (GameObject bullet in cylinderBullets)
        {

            if (bullet.GetComponent<Identification>().id == bullets)
            {

                currentBullet = bullet;

            }

        }

        Reload();

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
                if (primed && loaded && bullets > 0) //*
                {
                    currentBullet.SetActive(false);
                    bullets--;
                    Fire();
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
        primed = false;
        triggerDown = true;
        //add pew sound here
        HammerSet(0f);
        Instantiate(effect, firePos);
        RaycastHit hit;
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
                    rb.AddForce(transform.forward * addForce);
                }

                var target = col.transform.parent.GetComponentInChildren<Target>();
                if (target != null)
                {
                    target.AddScore();
                }
            }
        }

        //if bullets is equal to current bullet's ID set current bullet inactive take one from bullets and replace current bullet
    }

    //Reload function: Pops out the cylinder for reloading.
    public void Reload()
    {

        if (loaded == true && Input.GetButtonDown("Reload"))
        {

            cylinder.transform.localEulerAngles = new Vector3(0f, 0f, 80f);

        }
        else if (loaded == false && Input.GetButtonDown("Reload"))
        {

            cylinder.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

        }

    }
}