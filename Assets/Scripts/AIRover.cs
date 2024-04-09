using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIRover : AIController
{
    // Create a variable to store the chase distance
    public float chaseDistance;

    // Create a variable to store the attack distance
    public float attackDistance;

    // Array of waypoint transforms to patrol
    public Transform[] waypoints;

    // Create a distance to store the next waypoint distance
    public float pointStopDistance;

    // Private variable to store the current waypoint
    private int currentWaypoint = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        // Set the current state to PATROL
        currentState = AIState.CHOOSETARGET;
    }

    // Update is called once per frame
    public override void Update()
    {
        // Make decisions
        MakeDecisions();
    }

    public override void MakeDecisions()
    {
        switch(currentState)
        {
            case AIState.CHOOSETARGET:
                {
                    // Do actions for the CHOOSETARGET state
                    Debug.Log("Sentinel is choosing a target...");
                    DoChooseTargetState();
                    // Set the AI state to PATROL
                    ChangeState(AIState.PATROL);
                    break;
                }
            case AIState.PATROL:
                {
                    // Do actions for the PATROL state
                    Debug.Log("Rover is patrolling...");
                    DoPatrolState();
                    // Check to see if player can be heard
                    if (CanHear(targetPlayer))
                    {
                        ChangeState(AIState.SCAN);
                    }
                    // Check to see if player can be seen
                    if (CanSee(targetPlayer))
                    {
                        ChangeState(AIState.CHASE);
                    }
                    break;
                }
            case AIState.CHASE:
                {
                    // Do actions for the CHASE state
                    Debug.Log("Rover is chasing...");
                    DoChaseState(targetPlayer);
                    // Check if the player is too far away
                    if (!IsDistanceLessThan(targetPlayer, chaseDistance) && !CanHear(targetPlayer) && !CanSee(targetPlayer))
                    {
                        ChangeState(AIState.PATROL);
                    }
                    // Check if the player is close enough to attack
                    if (IsDistanceLessThan(targetPlayer, attackDistance))
                    {
                        ChangeState(AIState.ATTACK);
                    }
                    break;
                }
                case AIState.ATTACK:
                {
                    // Do actions for the ATTACK state
                    Debug.Log("Rover is attacking...");
                    DoAttackState(targetPlayer);
                    // Check if the player is too far away
                    if (!IsDistanceLessThan(targetPlayer, attackDistance) && !CanHear(targetPlayer) && !CanSee(targetPlayer))
                    {
                        ChangeState(AIState.CHASE);
                    }
                    break;
                }
                case AIState.RETURNTOPATROL:
                {
                    // Do actions for the RETURNTOPATROL state
                    Debug.Log("Rover is returning to patrol...");
                    DoReturnToPatrolState();
                    // Check to see if player can be heard
                    if (CanHear(targetPlayer))
                    {
                        ChangeState(AIState.SCAN);
                    }
                    // Check to see if player can be seen
                    if (CanSee(targetPlayer))
                    {
                        ChangeState(AIState.CHASE);
                    }
                    break;
                }
        }
    }

    public void DoPatrolState()
    {
        Patrol();
    }
    public void DoReturnToPatrolState()
    {
        ReturnToPatrol();
    }

    public void Patrol()
    {
        // If we have enough waypoints in our list to move to a current waypoint...
        if (waypoints.Length > currentWaypoint)
        {
            // ...then seek that waypoint
            Chase(waypoints[currentWaypoint]);
            // ...if we are close enough to the waypoint, increment to the next waypoint
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < pointStopDistance)
            {
                currentWaypoint++;
            }
        }
    }

    public void ReturnToPatrol()
    {
        // Return to nearest waypoint and restart patrol
        currentWaypoint = GetNearestWaypoint();
        ChangeState(AIState.PATROL);
    }

    int GetNearestWaypoint()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            return 0;
        }

        // Iterate through the waypoints and find the closest waypoint index
        int closestWaypoint = 0;
        float closestDistance = Vector3.Distance(pawn.transform.position, waypoints[0].position);
        for (int i = 1; i < waypoints.Length; i++)
        {
            float distance = Vector3.Distance(pawn.transform.position, waypoints[i].position);
            if (distance < closestDistance)
            {
                closestWaypoint = i;
                closestDistance = distance;
            }
        }

        return closestWaypoint;
    }
}
