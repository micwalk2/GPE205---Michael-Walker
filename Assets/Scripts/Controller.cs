using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Controller : MonoBehaviour
{
    // Variable to hold our pawn
    public Pawn pawn;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    // Child classes MAY override the way they process input (AIController does not process input)
    public virtual void ProcessInput(){}
}
