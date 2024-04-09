using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TankPawn : Pawn
{
    // Variable to hold the NoiseMaker component
    public NoiseMaker noiseMaker;

    // Private variable to hold the next time it can shoot
    private float nextShootTime;

    // Start is called before the first frame update
    public override void Start()
    {
        // If we have a GameManager...
        if (GameManager.instance != null)
        {
            // ...and it tracks the pawns...
            if (GameManager.instance.pawns != null)
            {
                // ...register with the GameManager
                GameManager.instance.pawns.Add(this);
            }
        }

        // Get the NoiseMaker component
        noiseMaker = GetComponent<NoiseMaker>();

        // Set nextShootTime to the current time
        nextShootTime = Time.time;

        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public void OnDestroy()
    {
        // If we have a GameManager...
        if (GameManager.instance != null)
        {
            // ...and it tracks the pawns...
            if (GameManager.instance.pawns != null)
            {
                // ...reregister from the GameManager
                GameManager.instance.pawns.Remove(this);
            }
        }
    }

    // Move the TankPawn forward
    public override void MoveForward()
    {
        // Check if the mover is null
        if (mover != null)
        {
            // Move the TankPawn forward
            mover.Move(transform.forward, moveSpeed);
            // Set NoiseMaker to make noise
            noiseMaker.currentVolume = noiseMaker.volumeDistance;
        }
        else
        {
            Debug.LogWarning("WARNING: No Mover in TankPawn.MoveForward()!");
        }

        // Set the NoiseMaker to stop making noise
        noiseMaker.currentVolume = 0f;
    }

    public override void MoveBackward()
    {
        // Check if the mover is null
        if (mover != null)
        {
            // Move the TankPawn backward
            mover.Move(transform.forward, -moveSpeed);
            // Set NoiseMaker to make noise
            noiseMaker.currentVolume = noiseMaker.volumeDistance;
        }
        else
        {
            Debug.LogWarning("WARNING: No Mover in TankPawn.MoveBackward()!");
        }

        // Set the NoiseMaker to stop making noise
        noiseMaker.currentVolume = 0f;

    }

    public override void RotateClockwise()
    {
        // Check if the mover is null
        if (mover != null)
        {
            // Rotate the TankPawn clockwise
            mover.Rotate(turnSpeed);
            // Set NoiseMaker to make noise
            noiseMaker.currentVolume = noiseMaker.volumeDistance;
        }
        else
        {
            Debug.LogWarning("WARNING: No Mover in TankPawn.RotateClockwise()!");
        }

        // Set the NoiseMaker to stop making noise
        noiseMaker.currentVolume = 0f;
    }

    public override void RotateCounterClockwise()
    {
        // Check if the mover is null
        if (mover != null)
        {
            // Rotate the TankPawn counterclockwise
            mover.Rotate(-turnSpeed);
            // Set NoiseMaker to make noise
            noiseMaker.currentVolume = noiseMaker.volumeDistance;
        }
        else
        {
            Debug.LogWarning("WARNING: No Mover in TankPawn.RotateCounterClockwise()!");
        }

        // Set the NoiseMaker to stop making noise
        noiseMaker.currentVolume = 0f;
    }

    public override void Shoot()
    {
        // Calculate the number of seconds per shot
        float secondsPerShot = 1 / shotsPerSecond;

        // Check to see if it's time to shoot...
        if (Time.time > nextShootTime)
        {
            // ...shoot and reset the timer
            shooter.Shoot(shellPrefab, fireForce, damageDone, lifespan);
            nextShootTime = Time.time + secondsPerShot;
            // Set NoiseMaker to make noise
            noiseMaker.currentVolume = noiseMaker.volumeDistance * 1.5f;
        }
        else
        {
            // ...otherwise, log a warning
            Debug.LogWarning("WARNING: TankPawn.Shoot() called too soon!");
        }

        // Set the NoiseMaker to stop making noise
        noiseMaker.currentVolume = 0f;
    }

    public override void RotateTowards(Vector3 targetPosition)
    {
        // Find the vector to our target
        Vector3 vectorToTarget = targetPosition - transform.position;
        // Find the rotation to look down that vector
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        // Rotate closer to that vector, but don't rotate more than turn speed allows in a single frame
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
