using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[FirestoreData]
public struct Quiz
{
    [FirestoreProperty]
    public string quizId { get; set; }
    [FirestoreProperty]
    public string quizName { get; set; }
    [FirestoreProperty]
    public string quizPoints { get; set; }
    [FirestoreProperty]
    public DateTime createdAt { get; set; }
    [FirestoreProperty]
    public Boolean isEnabled { get; set; }
    [FirestoreProperty]
    public Chapters chapter { get; set; }
    [FirestoreProperty]
    public string teacherEmail { get; set; }
}
