using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CircuitItem
{
    public string positionType { get; set; }
    public Vector3 positionVector { get; set; }
    public Quaternion rotationVector { get; set; }
    public bool isCorrectPosition { get; set; }
    public string itemName { get; set; }
}
