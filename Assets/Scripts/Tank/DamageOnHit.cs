using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public float damageValue;
    public Pawn owner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        // Get Health component from the object that was hit
        Health otherHealth = other.gameObject.GetComponent<Health>();

        // Check to see if the object has a Health component
        if (otherHealth != null)
        {
            // Do damage to the object
            otherHealth.TakeDamage(damageValue, owner);
        }

        // Destroy ourselves, whether we did damage or not
        Destroy(gameObject);
    }
}
