using Firebase.Firestore;
using System;
using UnityEngine;

[FirestoreData]
public struct Grades
{
    [FirestoreProperty]
    public DateTime createdAt { get; set; }
    [FirestoreProperty]
    public float grade { get; set; }
    [FirestoreProperty]
    public Student student { get; set; }
    [FirestoreProperty]
    public Quiz quiz { get; set; }

    void SetGrade(float value)
    {
        // Round to 1 decimal place before assignment
        grade = Mathf.Round(value * 10f) / 10f;
    }
}
