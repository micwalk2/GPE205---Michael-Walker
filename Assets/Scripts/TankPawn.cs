using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    // Move the TankPawn forward
    public override void MoveForward()
    {
        // TODO: Implement MoveForward
        Debug.Log("MoveForward");
    }

    public override void MoveBackward()
    {
        // TODO: Implement MoveBackward
        Debug.Log("MoveBackward");
    }

    public override void RotateClockwise()
    {
        // TODO: Implement RotateClockwise
        Debug.Log("RotateClockwise");
    }

    public override void RotateCounterClockwise()
    {
        // TODO: Implement RotateCounterClockwise
        Debug.Log("RotateCounterClockwise");
    }
}
