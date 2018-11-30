using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This scrip was intended for the BigCowBoy walking around the town, but it could be applied to other characters as well.
/// Also attached to the horse, gameobjects will require animator and navmeshagent.
/// - Ken
/// </summary>

[RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
public class BigCowBoyWalk : MonoBehaviour {
    Animator animator;

    [SerializeField] Transform[] destinationPoints;
    int destinationPoint = 0;

    NavMeshAgent navAgent;

    bool walking = true;

    float idleTime = 3f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();

        navAgent.autoBraking = false;

    }

    // Stop the navmesh causing errors on disable.
    private void OnDisable()
    {
        navAgent = null;
    }

    void Update () {

        // Stop the navagent from moving, make animation Idle.
        if (navAgent.remainingDistance < 0.5f && walking == true)
        {
            navAgent.isStopped = true;
            Invoke("MoveToPoint", idleTime);
            walking = false;
        }

        animator.SetBool("walking", walking);

    }


    // Moves character to the destination point.
    void MoveToPoint()
    {
        // Stop the navmesh causing errors on disable.
        if (navAgent == null)
            return;

        navAgent.isStopped = false;
        walking = true;

        if (destinationPoints.Length == 0)
        {
            Debug.LogError("No destination points setup");
            return;
        }

        navAgent.destination = destinationPoints[destinationPoint].position;

        // Randomly assign the next destination point. 
        destinationPoint = Random.Range(0, destinationPoints.Length);

    }
}
