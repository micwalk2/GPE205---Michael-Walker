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
        SCAN,
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

    // Create a public float variable to store the hearing distance
    public float hearingDistance;

    // Create a public float variable to store the field of view
    public float fieldOfView;
    // Create a public float variable to store the vision distance
    public float visionDistance;

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

    public void DoScanState()
    {
        // Rotate the pawn to "look" for the player
        pawn.RotateCounterClockwise();
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

    public bool CanHear(GameObject target)
    {
        // Get the target's NoiseMaker component
        NoiseMaker targetNoiseMaker = target.GetComponent<NoiseMaker>();

        // If the target doesn't have a NoiseMaker component...
        if (targetNoiseMaker == null)
        {
            // ...then return false
            return false;
        }
        // If the target is making no noise, target can also not be heard...
        if (targetNoiseMaker.currentVolume <= 0)
        {
            return false;
        }
        // If they are making noise, add the VolumeDistance in the NoiseMaker to the hearingDistance of this AI
        float totalDistance = targetNoiseMaker.currentVolume + hearingDistance;
        // If the distance between our pawn and the target is less than the total distance...
        if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            // ...we can hear the target
            return true;
        }
        else
        {
            // ...otherwise, we can't hear the target
            return false;
        }
    }

    public bool CanSee(GameObject target)
    {
        // Create a RaycastHit variable to store information about what we hit
        RaycastHit hit;

        // Find the vector from the AI to the target
        Vector3 AIToTargetVector = target.transform.position - transform.position;

        // Find the angle between the direction our AI is facing (forward) and the vector to the target
        float angleToTarget = Vector3.Angle(AIToTargetVector, pawn.transform.forward);

        // If that angle is less than our field of view...
        if ((angleToTarget < fieldOfView))
        {
            // ...and we cast a ray from the AI to the target...
            if (Physics.Raycast(transform.position, AIToTargetVector, out hit, visionDistance))
            {
                // ...and we hit the target
                if (hit.transform.gameObject == target)
                {
                    // ...then we can see the target
                    return true;
                }
                // ...raycast did not hit the target, we cannot see the target
                else
                {
                    return false;
                }
            }
            else
            {
                // ...raycast did not hit anything, we cannot see the target
                return false;
            }
        }
        else
        {
            // ...the angle is not in our field of view, we cannot see the target
            return false;
        }
    }
}
