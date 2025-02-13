using Firebase.Firestore;
using System;
using UnityEngine;

[FirestoreData]
public struct Exercise
{
    [FirestoreProperty]
    public string exerciseId { get; set; }
    [FirestoreProperty]
    public string exerciseName { get; set; }
    [FirestoreProperty]
    public string exerciseTitle { get; set; }
    [FirestoreProperty]
    public string exerciseImg { get; set; }
    [FirestoreProperty]
    public string exerciseInstructions { get; set; }
    [FirestoreProperty]
    public string exercisePoints { get; set; }
    [FirestoreProperty]
    public float grade { get; set; }

    void SetGrade(float value)
    {
        // Round to 1 decimal place before assignment
        grade = Mathf.Round(value * 100f) / 100f;
    }
}
