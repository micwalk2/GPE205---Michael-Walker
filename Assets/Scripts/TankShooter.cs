using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : Shooter
{
    // Get the Transform of the fire point
    public Transform firepointTransform;

    // Start is called before the first frame update
    public override void Start()
    {
        // Get the Transform of FirepointTransform in prefab
        firepointTransform = this.transform.Find("FirepointTransform");
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void Shoot(GameObject shellPrefab, float fireForce, float damageDone, float lifespan)
    {
        // Instantiate our projectile
        GameObject newShell = Instantiate(shellPrefab, firepointTransform.position, firepointTransform.rotation) as GameObject;

        // Get the DamageOnHit component of the projectile
        DamageOnHit damageOnHit = newShell.GetComponent<DamageOnHit>();

        // If it has a DamageOnHit component...
        if (damageOnHit != null)
        {
            // ...set the damageDone value in the DamageOnHit component to the value that was passed in
            damageOnHit.damageValue = damageDone;
            // ...set the owner to the pawn that shot this shell, if there is one
            damageOnHit.owner = GetComponent<Pawn>();
        }

        // Get the rigidbody of the projectile
        Rigidbody rb = newShell.GetComponent<Rigidbody>();

        // If it has a rigidbody...
        if (rb != null)
        {
            // ...add force to the rigidbody in the direction of the firepointTransform's forward vector
            rb.AddForce(firepointTransform.forward * fireForce, ForceMode.Impulse);
        }

        // Destroy the projectile after the lifespan has expired
        Destroy(newShell, lifespan);
    }
}
