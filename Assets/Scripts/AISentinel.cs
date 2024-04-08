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
                    // Do actions for the CHOOSETARGET state
                    Debug.Log("Sentinel is choosing a target...");
                    DoChooseTargetState();
                    // Check if the player is close enough to chase
                    if (IsDistanceLessThan(targetPlayer, chaseDistance))
                    {
                        ChangeState(AIState.CHASE);
                    }
                    else
                    {
                        ChangeState(AIState.IDLE);
                    }
                    break;
                }
            case AIState.IDLE:
                {
                    // Do actions for the IDLE state
                    Debug.Log("Sentinel is idle...");
                    DoIdleState();
                    // Check if the player is close enough to chase
                    if (IsDistanceLessThan(targetPlayer, chaseDistance))
                    {
                        ChangeState(AIState.CHASE);
                    }
                    break;
                }
            case AIState.CHASE:
                {
                    // Do actions for the CHASE state
                    Debug.Log("Sentinel is chasing...");
                    DoChaseState(targetPlayer);
                    // Check if the player is too far away
                    if (!IsDistanceLessThan(targetPlayer, chaseDistance))
                    {
                        ChangeState(AIState.RETURNTOPOST);
                    }
                    // Check if the player is close enough to attack
                    if (IsDistanceLessThan(targetPlayer, attackDistance))
                    {
                        ChangeState(AIState.ATTACK);
                    }
                    // Check if the Sentinel is at its post
                    if (IsDistanceLessThan(targetPost, postDistance))
                    {
                        ChangeState(AIState.IDLE);
                    }
                    break;
                }
            case AIState.ATTACK:
                {
                    // Do actions for the ATTACK state
                    Debug.Log("Sentinel is attacking...");
                    DoAttackState(targetPlayer);
                    // Check if the player is too far away
                    if (!IsDistanceLessThan(targetPlayer, chaseDistance))
                    {
                        ChangeState(AIState.RETURNTOPOST);
                    }
                    // Check if the player is close enough to chase
                    if (IsDistanceLessThan(targetPlayer, chaseDistance))
                    {
                        ChangeState(AIState.CHASE);
                    }
                    break;
                }
            case AIState.RETURNTOPOST:
                {
                    // Do actions for the RETURNTOPOST state
                    Debug.Log("Sentinel is returning to post...");
                    DoReturnToPostState(targetPost);
                    // Check if the Sentinel is at its post
                    if (IsDistanceLessThan(targetPost, postDistance))
                    {
                        ChangeState(AIState.IDLE);
                    }
                    // Check if the player is close enough to chase
                    if (IsDistanceLessThan(targetPlayer, chaseDistance))
                    {
                        ChangeState(AIState.CHASE);
                    }
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
