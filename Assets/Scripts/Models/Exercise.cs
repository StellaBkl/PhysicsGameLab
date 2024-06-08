using Firebase.Firestore;
using System;

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
}
