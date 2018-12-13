using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public int points;
    public float reactionStartTime;
    bool triggered = false;
    HingeJoint hinge;
    JointSpring hingeSpring;
    ShootingGameController gameController;
    public GameObject speedBonus;

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
        reactionStartTime = Time.time;
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
            float speed = Time.time - reactionStartTime;
            if (speed > gameController.speedThreshold)
            {
                gameController.Addscore(points);
            }
            else
            {
                gameController.Addscore(points, speed);
                Instantiate(speedBonus, transform.position, Quaternion.identity);
            }
            FlipDown();
        }
    }
}
