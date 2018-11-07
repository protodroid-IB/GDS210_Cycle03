using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float aimSpeed;
    public float hammerTension;
    public float hammerSensitivity;
    public float hammerMax;
    public float addForce;

    [HideInInspector] public bool aim;
    [HideInInspector] public bool primed;
    [HideInInspector] public float hammerPull;

    public GameObject hammer;
    public GameObject cylinder;

    public Transform hipPos;
    public Transform aimPos;
    public Transform firePos;

    public GameObject effect;

    Vector3 pos;
    Quaternion rot;

    private void Update()
    {
        if (aim)
        {
            pos = aimPos.position;
            rot = aimPos.rotation;
        }
        else
        {
            pos = hipPos.position;
            rot = hipPos.rotation;
        }
        transform.position = Vector3.Lerp(transform.position, pos, aimSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, aimSpeed * Time.deltaTime);

        if (!primed)
        {
            float angle;
            if (hammerPull < 0f)
            {
                angle = Mathf.MoveTowardsAngle(hammer.transform.localEulerAngles.x, hammerMax, -hammerPull * hammerSensitivity * Time.deltaTime);
                if (hammer.transform.localEulerAngles.x == hammerMax)
                {
                    primed = true;
                    angle = hammerMax;
                }
            }
            else
            {
                angle = Mathf.LerpAngle(hammer.transform.localEulerAngles.x, 0f, hammerTension * Time.fixedDeltaTime);
            }
            hammer.transform.localEulerAngles = new Vector3(angle, 0f, 0f);
        }
    }

    public void Fire()
    {
        primed = false;
        hammer.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        Instantiate(effect, firePos);
        RaycastHit hit;
        if (Physics.Raycast(firePos.transform.position, transform.forward, out hit,1<<8))
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
            }
        }
    }
}
