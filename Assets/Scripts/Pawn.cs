using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Pawn : MonoBehaviour
{
    // Variable for move speed
    public float moveSpeed;
    // Variable for turn speed
    public float turnSpeed;
    // Variable to hold Mover component
    public Mover mover;
    // Variable to hold Shooter component
    public Shooter shooter;
    // Variable to hold shell prefab
    public GameObject shellPrefab;
    // Variable to hold firing force
    public float fireForce;
    // Variable to hold damage done
    public float damageDone;
    // Variable to hold lifespan of projectile
    public float lifespan;
    // Variable for rate of fire
    public float shotsPerSecond;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // Get the Mover component
        mover = GetComponent<Mover>();
        // Get the Shooter component
        shooter = GetComponent<Shooter>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void Shoot();
    public abstract void RotateTowards(Vector3 targetPosition);
}
