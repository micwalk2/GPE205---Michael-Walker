using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    // Store a reference to the powerup
    public HealthPowerup healthPowerup;

    public override void OnTriggerEnter(Collider other)
    {
        // Store the other object's PowerupManager component
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();

        // If the other object has a PowerupManager component...
        if (powerupManager != null)
        {
            // ...add the health powerup to the PowerupManager
            powerupManager.Add(healthPowerup);

            // ...destroy the health pickup
            Destroy(gameObject);
            Debug.Log(gameObject + "destroyed!");
        }
        else
        {
            // ...otherwise, object doesn't have a PowerupManager
            Debug.Log(other.name + " has no PowerupManager!");
        }
    }
}
