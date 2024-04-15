using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TankMover : Mover
{
    // Variable to hold the Rigidbody component
    private Rigidbody rb;
    private Transform tf;

    // Start is called before the first frame update
    public override void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Get transform component
        tf = GetComponent<Transform>();
    }

    public override void Move(Vector3 moveDirection, float moveSpeed)
    {
        // Freeze the rotation of the tank mover
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Move the tank mover in the direction of the moveDirection vector
        Vector3 moveVector = moveDirection.normalized * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVector);
    }

    public override void Rotate(float rotateSpeed)
    {
        // Rotate the tank mover
        tf.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
}
