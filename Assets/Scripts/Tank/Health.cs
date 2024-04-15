using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Public health variables
    public float currentHealth;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Set the current health to the max health
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Heal(float amount, Pawn pawnThatHealed)
    {
        Debug.Log(pawnThatHealed + " healed you for " + amount + "!");

        // Add the amount to the current health
        currentHealth += amount;

        // If the current health is greater than the max health...
        if (currentHealth > maxHealth)
        {
            // ...set the current health to the max health
            currentHealth = maxHealth;
        }
    }

    // Method to take damage
    public void TakeDamage(float damage, Pawn source)
    {
        // Check to see if the object is already dead
        if (currentHealth <= 0)
        {
            Die(source);
            Debug.Log(gameObject.name + " is already dead!  Destroying " + gameObject.name + ".");
        }
        // If the object is not dead, take damage
        else
        {
            currentHealth -= damage;
            Debug.Log(source.name + " dealt " + damage + " damage to " + gameObject.name + ".");
        }

        // Check to see if the object is dead
        if (currentHealth <= 0)
        {
            Die(source);
            Debug.Log(gameObject.name + " has died!  Destroying " + gameObject.name + ".");
        }
    }

    // Destroy the object
    public void Die(Pawn source)
    {
        // TODO: Implement reward system for damage source

        // Destroy the object
        Destroy(gameObject);
    }
}
