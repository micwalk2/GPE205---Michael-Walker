using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TankPawn : Pawn
{
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
        }
        else
        {
            Debug.LogWarning("WARNING: No Mover in TankPawn.RotateCounterClockwise()!");
        }
    }
}
