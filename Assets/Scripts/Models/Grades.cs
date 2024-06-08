using Firebase.Firestore;
using System;

[FirestoreData]
public struct Grades
{
    [FirestoreProperty]
    public string chapterId { get; set; }
    [FirestoreProperty]
    public string chapterName { get; set; }
    [FirestoreProperty]
    public DateTime createdAt { get; set; }
    [FirestoreProperty]
    public float grade { get; set; }
    [FirestoreProperty]
    public Student student { get; set; }
    [FirestoreProperty]
    public Exercise exercise { get; set; }
}
