using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPowerup : Powerup
{
    public float healthToAdd;

    public override void Apply(PowerupManager target)
    {
        // Get the target's Health component
        Health targetHealth = target.GetComponent<Health>();

        // If the target has a Health component...
        if (targetHealth != null)
        {
            // ...apply the health changes
            targetHealth.Heal(healthToAdd, target.GetComponent<Pawn>());
        }
    }

    public override void Remove(PowerupManager target)
    {
        // TODO: Remove powerup changes
    }
}
