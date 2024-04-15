using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TankPawn : Pawn
{
    // Variable to hold the NoiseMaker component
    [HideInInspector] public NoiseMaker noiseMaker;

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

            // Check to see if there is a NoiseMaker component
            if (noiseMaker != null)
            {
                // Check if the player is moving
                if (mover.transform.hasChanged)
                {
                    // If the player is moving, set NoiseMaker to make noise and isMoving to true
                    noiseMaker.currentVolumeDistance = noiseMaker.volumeDistance;
                    noiseMaker.isMoving = true;
                }
                else
                {
                    // If the player is not moving, set NoiseMaker volume to 0 and isMoving to false
                    noiseMaker.currentVolumeDistance = 0f;
                    noiseMaker.isMoving = false;
                }

                // Reset the hasChanged flag
                mover.transform.hasChanged = false;
            }
        }
        else
        {
            Debug.LogWarning("WARNING: No Mover in TankPawn.MoveForward()!");
        }
    }

    public override void MoveBackward()
    {
        // Check if the mover is null
        if (mover != null)
        {
            // Move the TankPawn backward
            mover.Move(transform.forward, -moveSpeed);

            // Check to see if there is a NoiseMaker component
            if (noiseMaker != null)
            {
                // Check if the player is moving
                if (mover.transform.hasChanged)
                {
                    // If the player is moving, set NoiseMaker to make noise and isMoving to true
                    noiseMaker.currentVolumeDistance = noiseMaker.volumeDistance;
                    noiseMaker.isMoving = true;
                }
                else
                {
                    // If the player is not moving, set NoiseMaker volume to 0 and isMoving to false
                    noiseMaker.currentVolumeDistance = 0f;
                    noiseMaker.isMoving = false;
                }

                // Reset the hasChanged flag
                mover.transform.hasChanged = false;
            }
        }
        else
        {
            Debug.LogWarning("WARNING: No Mover in TankPawn.MoveBackward()!");
        }

    }

    public override void RotateClockwise()
    {
        // Check if the mover is null
        if (mover != null)
        {
            // Rotate the TankPawn clockwise
            mover.Rotate(turnSpeed);

            // Check to see if there is a NoiseMaker component
            if (noiseMaker != null)
            {
                // Check if the player is moving
                if (mover.transform.hasChanged)
                {
                    // If the player is moving, set NoiseMaker to make noise and isMoving to true
                    noiseMaker.currentVolumeDistance = noiseMaker.volumeDistance;
                    noiseMaker.isMoving = true;
                }
                else
                {
                    // If the player is not moving, set NoiseMaker volume to 0 and isMoving to false
                    noiseMaker.currentVolumeDistance = 0f;
                    noiseMaker.isMoving = false;
                }

                // Reset the hasChanged flag
                mover.transform.hasChanged = false;
            }
        }
        else
        {
            Debug.LogWarning("WARNING: No Mover in TankPawn.RotateClockwise()!");
        }
    }

    public override void RotateCounterClockwise()
    {
        // Check if the mover is null
        if (mover != null)
        {
            // Rotate the TankPawn counterclockwise
            mover.Rotate(-turnSpeed);

            // Check to see if there is a NoiseMaker component
            if (noiseMaker != null)
            {
                // Check if the player is moving
                if (mover.transform.hasChanged)
                {
                    // If the player is moving, set NoiseMaker to make noise and isMoving to true
                    noiseMaker.currentVolumeDistance = noiseMaker.volumeDistance;
                    noiseMaker.isMoving = true;
                }
                else
                {
                    // If the player is not moving, set NoiseMaker volume to 0 and isMoving to false
                    noiseMaker.currentVolumeDistance = 0f;
                    noiseMaker.isMoving = false;
                }

                // Reset the hasChanged flag
                mover.transform.hasChanged = false;
            }
        }
        else
        {
            Debug.LogWarning("WARNING: No Mover in TankPawn.RotateCounterClockwise()!");
        }
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
            
            // Check to see if there is a NoiseMaker component
            if (noiseMaker != null)
            {
                // Set NoiseMaker to make noise from shooting
                noiseMaker.currentVolumeDistance = noiseMaker.volumeDistance * 1.5f;

                // Reset the NoiseMaker volume
                noiseMaker.currentVolumeDistance = 0f;
            }
        }
        else
        {
            // ...otherwise, log a warning
            Debug.LogWarning("WARNING: TankPawn.Shoot() called too soon!");
        }
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
