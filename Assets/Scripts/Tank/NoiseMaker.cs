using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    public float volumeDistance;
    [HideInInspector] public float currentVolumeDistance;
    [HideInInspector] public bool isMoving = false;
}