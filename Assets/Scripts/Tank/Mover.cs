using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    // Methods to be overriden by child classes
    public abstract void Start();
    public abstract void Move(Vector3 moveDirection, float moveSpeed);
    public abstract void Rotate(float rotateSpeed);
}
