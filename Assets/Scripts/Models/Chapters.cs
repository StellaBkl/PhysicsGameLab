using Firebase.Firestore;
using System;

[FirestoreData]
public struct Chapters
{
    [FirestoreProperty]
    public string chapterId { get; set; }
    [FirestoreProperty]
    public string chapterName { get; set; }
    [FirestoreProperty]
    public Exercise[] exercises { get; set; }
}
