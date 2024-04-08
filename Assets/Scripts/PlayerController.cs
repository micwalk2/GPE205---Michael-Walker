using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : Controller
{
    // Variables to hold our player input
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode shootKey;

    // Start is called before the first frame update
    public override void Start()
    {
        // If we have a GameManager...
        if(GameManager.instance != null)
        {
            // ...and it tracks the players...
            if(GameManager.instance.players != null)
            {
                // ...register with the GameManager
                GameManager.instance.players.Add(this);
            }
        }

        // Run the Start method from the base class
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Process player input
        ProcessInput();
        
        // Run the Update method from the base class
        base.Update();
    }

    // Called when object is destroyed
    public void OnDestroy()
    {
        // If we have a GameManager...
        if(GameManager.instance != null)
        {
            // ...and it tracks the players...
            if(GameManager.instance.players != null)
            {
                // ...reregister from the GameManager
                GameManager.instance.players.Remove(this);
            }
        }
    }

    public override void ProcessInput()
    {
        // Player input processing
        if (Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
        }
        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
        }
        if (Input.GetKey(rotateClockwiseKey))
        {
            pawn.RotateClockwise();
        }
        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            pawn.RotateCounterClockwise();
        }
        if (Input.GetKeyDown(shootKey))
        {
            pawn.Shoot();
        }
    }
}
