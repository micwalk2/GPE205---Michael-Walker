using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISentinel : AIController
{
    // Create a variable to store the chase distance
    public float chaseDistance;

    // Create a variable to store the attack distance
    public float attackDistance;

    // Create a distance to store the post distance
    public float postDistance;

    // Create a variable to store a GameObject that is the post to return to
    public GameObject targetPost;

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
                    // Execute CHOOSETARGET state:
                    Debug.Log("Sentinel is choosing a target!");
                    DoChooseTargetState();

                    // Change to IDLE state
                    ChangeState(AIState.IDLE);

                    break;
                }
            case AIState.IDLE:
                {
                    // Execute IDLE state:
                    Debug.Log("Sentinel is idle!");
                    DoIdleState();

                    // Check if the target is heard
                    if (CanHear(targetPlayer))
                    {
                        Debug.Log("Sentinel heard the target!");

                        // Change to SCAN state:
                        ChangeState(AIState.SCAN);
                    }

                    break;
                }
            case AIState.SCAN:
                {
                    // Execute the SCAN state:
                    Debug.Log("Sentinel is scanning!");
                    DoScanState();

                    if (CanSee(targetPlayer))
                    {
                        Debug.Log("Sentinel sees the target!");
                        ChangeState(AIState.CHASE);
                    }
                    // If the target is neither heard no seen, return to post
                    else if (!CanHear(targetPlayer) && !CanSee(targetPlayer))
                    {
                        Debug.Log("Sentinel lost the target!");
                        ChangeState(AIState.IDLE);
                    }

                    break;
                }
            case AIState.CHASE:
                {
                    // Execute the CHASE state:
                    Debug.Log("Sentinel is chasing!");
                    DoChaseState(targetPlayer);

                    // Check to see if the target is still seen
                    if (!CanSee(targetPlayer))
                    {
                        Debug.Log("Sentinel no longer sees the target!");
                        ChangeState(AIState.RETURNTOPOST);
                    }

                    // Check to see if the target is close enough to attack
                    if (IsDistanceLessThan(targetPlayer, attackDistance))
                    {
                        Debug.Log("Sentinel is close enough to attack!");
                        ChangeState(AIState.ATTACK);
                    }
                    break;
                }
            case AIState.ATTACK:
                {
                    // Execute the ATTACK state:
                    Debug.Log("Sentinel is attacking!");
                    DoAttackState(targetPlayer);

                    // Check to see if the target is still close enough to attack
                    if (!IsDistanceLessThan(targetPlayer, attackDistance))
                    {
                        Debug.Log("Sentinel is no longer close enough to attack!");
                        ChangeState(AIState.CHASE);
                    }

                    // Check to see if the target is still seen
                    if (!CanSee(targetPlayer))
                    {
                        Debug.Log("Sentinel no longer sees the target!");
                        ChangeState(AIState.RETURNTOPOST);
                    }

                    break;
                }
            case AIState.RETURNTOPOST:
                {
                    // Execute the RETURNTOPOST state:
                    Debug.Log("Sentinel is returning to post!");
                    DoReturnToPostState(targetPost);

                    break;
                }
        }
    }
    public void DoReturnToPostState(GameObject targetPost)
    {
        // Return to the target post
        Chase(targetPost.transform.position);
    }
}
