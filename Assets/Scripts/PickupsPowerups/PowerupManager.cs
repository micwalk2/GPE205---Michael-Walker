using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    // Declare the list of current powerups
    public List<Powerup> powerups;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the list of powerups
        powerups = new List<Powerup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(Powerup powerupToAdd)
    {
        // Add the powerup to the PowerupManager
        powerupToAdd.Apply(this);

        // Add the powerup to the list of powerups
        powerups.Add(powerupToAdd);
    }

    public void Remove(Powerup powerToRemove)
    {
        // TODO: Create the Remove() method
    }
}
