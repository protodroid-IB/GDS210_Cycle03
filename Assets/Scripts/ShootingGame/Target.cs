using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public int points;
    bool triggered = false;
    float timer = 0;
    HingeJoint hinge;
    JointSpring hingeSpring;
    ShootingGameController gameController;

    public void Awake()
    {
        gameController = FindObjectOfType<ShootingGameController>();
        hinge = GetComponent<HingeJoint>();
        hingeSpring = hinge.spring;
    }

    public void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer < 0)
        {
            FlipDown();
        }
    }

    public void FlipUp(float time = 0f)
    {
        triggered = true;
        SetAngle(0);
        timer = time;
    }

    public void FlipDown()
    {
        triggered = false;
        SetAngle(-80);
        timer = 0;
    }

    void SetAngle(float angle)
    {
        hingeSpring.targetPosition = angle;
        hinge.spring = hingeSpring;
    }

    public void AddScore()
    {
        if (triggered)
        {
            gameController.Addscore(points);
            FlipDown();
        }
    }
}
