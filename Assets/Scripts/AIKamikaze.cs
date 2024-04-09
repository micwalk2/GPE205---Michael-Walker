using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIKamikaze : AIController
{
    // Create a variable to store the chase distance
    public float chaseDistance;

    // Create a variable to store the explode distance
    public float explodeDistance;

    // Create a variable to store explosion damage
    public float explosionDamage;

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
        switch(currentState)
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
                    Debug.Log("Kamikaze is idle...");
                    DoIdleState();
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
                    break;
                }
            case AIState.CHASE:
                {
                    // Do actions for the CHASE state
                    Debug.Log("Kamikaze is chasing...");
                    DoChaseState(targetPlayer);
                    // Check if the player is too far away
                    if (!CanSee(targetPlayer))
                    {
                        ChangeState(AIState.IDLE);
                    }
                    // Check if the player is close enough to attack
                    if (IsDistanceLessThan(targetPlayer, explodeDistance))
                    {
                        ChangeState(AIState.EXPLODE);
                    }
                    break;
                }
                case AIState.EXPLODE:
                {
                    // Do actions for the EXPLODE state
                    Debug.Log("Kamikaze is exploding...");
                    DoExplodeState(targetPlayer, explosionDamage);
                    // No transitions to check for, object will be destroyed
                    break;
                }
        }
    }
    public void DoExplodeState(GameObject targetPlayer, float damage)
    {
        Explode(targetPlayer, damage);
    }

    public void Explode(GameObject targetPlayer, float damage)
    {
        // Deal damage to the target player
        targetPlayer.GetComponent<Health>().TakeDamage(damage, pawn);
        // Destroy the AI
        Destroy(pawn.gameObject);
    }
}
