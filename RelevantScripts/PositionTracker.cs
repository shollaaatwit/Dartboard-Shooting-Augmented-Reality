using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    public Vector3 originalPosition;
    void Awake()
    {
        originalPosition = transform.position;
    }

    public Vector3 returnPosition()
    {
        return originalPosition;
    }
}
