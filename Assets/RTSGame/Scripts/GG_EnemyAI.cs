using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GG_EnemyAI : MonoBehaviour
{
    //public float patrolSpeed = 2f;                          // The nav mesh agent's speed when patrolling.
    //public float chaseSpeed = 5f;                           // The nav mesh agent's speed when chasing.
    //public float chaseWaitTime = 5f;                        // The amount of time to wait when the last sighting is reached.
    //public float patrolWaitTime = 1f;                       // The amount of time to wait when the patrol way point is reached.
    public Transform[] patrolWayPoints;                     // An array of transforms for the patrol route.
    public CharacterController controller;

    //private DoneEnemySight enemySight;                      // Reference to the EnemySight script.
    //private NavMeshAgent nav;                               // Reference to the nav mesh agent.
    //private Transform player;                               // Reference to the player's transform.
    //private DonePlayerHealth playerHealth;                  // Reference to the PlayerHealth script.
    //private DoneLastPlayerSighting lastPlayerSighting;      // Reference to the last global sighting of the player.
    //private float chaseTimer;                               // A timer for the chaseWaitTime.
    //private float patrolTimer;                              // A timer for the patrolWaitTime.
    private int wayPointIndex;                              // A counter for the way point array.

    public Animator animator;
    private AnimatorSetup animatorSetup;
    private HashIDs hash;

    private GG_AIPath aiPath;

    void Awake()
    {
        // Setting up the references.
        //enemySight = GetComponent<DoneEnemySight>();
        //nav = GetComponent<NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag(DoneTags.player).transform;
        //playerHealth = player.GetComponent<DonePlayerHealth>();
        //lastPlayerSighting = GameObject.FindGameObjectWithTag(DoneTags.gameController).GetComponent<DoneLastPlayerSighting>();
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        // Set an appropriate speed for the NavMeshAgent.
        //nav.speed = patrolSpeed;

        /*
        controller.velocity = patrolSpeed;

        // If near the next waypoint or there is no destination...
        if (nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance)
        {
            // ... increment the timer.
            patrolTimer += Time.deltaTime;

            // If the timer exceeds the wait time...
            if (patrolTimer >= patrolWaitTime)
            {
                // ... increment the wayPointIndex.
                if (wayPointIndex == patrolWayPoints.Length - 1)
                    wayPointIndex = 0;
                else
                    wayPointIndex++;

                // Reset the timer.
                patrolTimer = 0;
            }
        }
        else
            // If not near a destination, reset the timer.
            patrolTimer = 0;

        // Set the destination to the patrolWayPoint.
        nav.destination = patrolWayPoints[wayPointIndex].position;
        */
    }

    public void NavSetup()
    {

    }
}
