using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMarksman : AIController
{
    // Create a variable to store the attack distance
    public float attackDistance;

    // Create a variable to store the attack damage
    public float attackDamage;

    // Create a variable to store the flee distance
    public float fleeDistance;

    // Start is called before the first frame update
    public override void Start()
    {
        // Set the current state to IDLE
        ChangeState(AIState.CHOOSETARGET);
    }

    // Update is called once per frame
    public override void Update()
    {
        // Make decisions
        MakeDecisions();
    }

    public override void MakeDecisions()
    {
        switch (currentState)
        {
            case AIState.CHOOSETARGET:
                {
                    // Do actions for the CHOOSETARGET state
                    Debug.Log("Sentinel is choosing a target...");
                    DoChooseTargetState();
                    // Check if the player can be heard
                    if (CanHear(targetPlayer))
                    {
                        ChangeState(AIState.SCAN);
                    }
                    // Check if the player can be seen
                    if (CanSee(targetPlayer))
                    {
                        ChangeState(AIState.CHASE);
                    }
                    // Set the AI state to IDLE
                    ChangeState(AIState.IDLE);
                    break;
                }
            case AIState.IDLE:
                {
                    // Do actions for the IDLE state
                    Debug.Log("Marksman is idle...");
                    DoIdleState();
                    // Check to see if the player can be heard
                    if (CanHear(targetPlayer))
                    {
                        ChangeState(AIState.SCAN);
                    }
                    // Check to see if the player can be seen
                    if (CanSee(targetPlayer) && IsDistanceLessThan(targetPlayer, attackDistance))
                    {
                        ChangeState(AIState.ATTACK);
                    }
                    // Check if the player is close enough to flee
                    if (IsDistanceLessThan(targetPlayer, fleeDistance))
                    {
                        ChangeState(AIState.FLEE);
                    }
                    break;
                }
            case AIState.ATTACK:
                {
                    // Do actions for the ATTACK state
                    Debug.Log("Marksman is attacking...");
                    DoAttackState(targetPlayer);
                    // Check if the player is too far away
                    if (!IsDistanceLessThan(targetPlayer, attackDistance))
                    {
                        ChangeState(AIState.IDLE);
                    }
                    // Check if the player is close enough to flee
                    if (IsDistanceLessThan(targetPlayer, fleeDistance))
                    {
                        ChangeState(AIState.FLEE);
                    }
                    break;
                }
            case AIState.FLEE:
                {
                    // Do actions for the FLEE state
                    Debug.Log("Marksman is fleeing...");
                    DoFleeState(targetPlayer, fleeDistance);
                    // Check if the player is too far away
                    if (!IsDistanceLessThan(targetPlayer, fleeDistance))
                    {
                        ChangeState(AIState.IDLE);
                    }
                    break;
                }
        }
    }

    public void DoFleeState(GameObject targetPlayer, float fleeDistance)
    {
        Flee(targetPlayer, fleeDistance);
    }

    public void Flee(GameObject targetPlayer, float fleeDistance)
    {
        // Find the distance to the target player
        float targetDistance = Vector3.Distance(targetPlayer.transform.position, pawn.transform.position);
        // Get the percent of flee distance
        float percentOfFleeDistance = targetDistance / fleeDistance;
        // Clamp the flee distance
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);
        // Get the flipped percent of flee distance
        float flippedPercentOfFleeDistance = 1 - percentOfFleeDistance;
        // Find the vector to the target player
        Vector3 vectorToTarget = pawn.transform.position - targetPlayer.transform.position;
        // Find the vector away from the target player
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        // Normalize the vector and multiply it by the flee distance
        Vector3 fleeVector = vectorAwayFromTarget.normalized * (fleeDistance * flippedPercentOfFleeDistance);
        // Flee from the target player
        Chase(pawn.transform.position + fleeVector);
    }
}
