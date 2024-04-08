using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    // Implement enum for states
    public enum AIState
    {
        IDLE,
        CHASE,
        ATTACK,
        RETURNTOPOST,
        EXPLODE,
        FLEE,
        PATROL,
        RETURNTOPATROL,
        CHOOSETARGET
    }

    // Create a variable to store the current state
    public AIState currentState;

    // Create a private float variable to store the time of last state change
    private float lastStateChangeTime;

    // Create a public GameObject variable that holds the target player
    public GameObject targetPlayer;

    // Start is called before the first frame update
    public override void Start()
    {
        // Run the parent class's Start method
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Run the parent class's Update method
        base.Update();
    }

    public virtual void MakeDecisions() { }

    public void ChangeState(AIState newState)
    {
        // Change the current state to the new state
        currentState = newState;
        // Save the time when we changed states
        lastStateChangeTime = Time.time;
    }

    public void DoIdleState()
    {
        // Do nothing
    }

    public void DoChaseState(GameObject targetPlayer)
    {
        // Chase the target player
        Chase(targetPlayer);
    }

    public void Chase(GameObject targetPlayer)
    {
        // Rotate towards the target
        pawn.RotateTowards(targetPlayer.transform.position);
        // Move towards the target
        pawn.MoveForward();
    }

    public void Chase(Vector3 targetPosition)
    {
        // Rotate towards the position
        pawn.RotateTowards(targetPosition);
        // Move forward
        pawn.MoveForward();
    }

    public void Chase(Transform targetTransform)
    {
        // Seek the position of our target transform
        Chase(targetTransform.position);
    }

    public void DoAttackState(GameObject targetPlayer)
    {
        // Face the target
        Transform transform = targetPlayer.transform;
        Vector3 directionToTarget = (transform.position - pawn.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);

        // This while loop will run until the pawn is facing the target
        while (Quaternion.Angle(transform.rotation, lookRotation) > 0.1f)
        {
            // Rotate the pawn towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        // Shoot at the target player
        pawn.Shoot();
    }

    public void DoChooseTargetState()
    {
        TargetPlayerOne();
    }

    public void TargetPlayerOne()
    {
        // If the GameManager exists
        if (GameManager.instance != null)
        {
            // ...and the array of players is not empty
            if (GameManager.instance.players != null)
            {
                // ...and there are players in it
                if (GameManager.instance.players.Count > 0)
                {
                    // ...then target the gameObject of the pawn of the first player controller in the list
                    targetPlayer = GameManager.instance.players[0].pawn.gameObject;
                }
            }
        }
    }

    public bool IsHasTarget()
    {
        // Return true if the target player is not null
        return (targetPlayer != null);
    }

    public bool IsDistanceLessThan(GameObject target, float distance)
    {
        // Check if the distance between the AI and the target is less than the specified distance
        if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
