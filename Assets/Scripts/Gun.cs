using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public float hammerTension;
    public float hammerSensitivity;
    public float hammerMax;
    public float addForce;

    bool primed;
    bool triggerDown;
    float hammerPull;
    float hammerVal;
    float hammerTarget;

    public GameObject hammer;
    public GameObject cylinder;

    public Transform firePos;

    public GameObject effect;

    private void Update()
    {
        if (!primed && hammerPull == 0f)
        {
            HammerSet(Mathf.Lerp(hammerVal, 0f, hammerTension * Time.deltaTime));
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
                if (primed)
                {
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
    }
}