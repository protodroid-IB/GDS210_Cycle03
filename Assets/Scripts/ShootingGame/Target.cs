using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public int points;
    bool triggered = false;
    HingeJoint hinge;
    JointSpring hingeSpring;
    ShootingGameController gameController;

    public void Awake()
    {
        gameController = FindObjectOfType<ShootingGameController>();
        hinge = GetComponent<HingeJoint>();
        hingeSpring = hinge.spring;
    }

    public void FlipUp(float time = 0f)
    {
        CancelInvoke();
        triggered = true;
        SetAngle(0);
        Invoke("FlipDown", time);
    }

    public void FlipDown()
    {
        triggered = false;
        SetAngle(-80);
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
