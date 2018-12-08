using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CowGirlWalk : MonoBehaviour {

    Animator animator;

    [SerializeField] Transform[] destinationPoints;
    int refPoint = 0;

    Vector3[] destinationPoint;

    NavMeshAgent navAgent;

    bool walking = true;

    //float idleTime = 3f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();

        destinationPoint = new Vector3[destinationPoints.Length];

        navAgent.autoBraking = false;

        for (int i = 0; i < destinationPoint.Length; i++)
        {
            destinationPoint[i] = destinationPoints[i].position;
        }
    }

    // Stop the navmesh causing errors on disable.
    private void OnDisable()
    {
        navAgent = null;
    }

    void Update()
    {
        // Stop the navagent from moving, make animation Idle.
        if (navAgent.remainingDistance < 0.5f)
        {
            MoveToPoint();
        }
    }


    // Moves character to the destination point.
    void MoveToPoint()
    {
        // Stop the navmesh causing errors on disable.
        if (navAgent == null)
            return;

        navAgent.isStopped = false;
        walking = true;

        if (destinationPoint.Length == 0)
        {
            Debug.LogError("No destination points setup");
            return;
        }

        navAgent.destination = destinationPoint[refPoint];

        // Randomly assign the next destination point. 
        refPoint = Random.Range(0, destinationPoint.Length);
    }
}
