using Firebase.Firestore;
using System;

[FirestoreData]
public struct Student
{
    [FirestoreProperty]
    public string studentId { get; set; }
    [FirestoreProperty]
    public string studentName { get; set; }
    [FirestoreProperty]
    public string studentSurname { get; set; }
    [FirestoreProperty]
    public string studentUsername { get; set; }
    [FirestoreProperty]
    public string passwordHash { get; set; }
    [FirestoreProperty]
    public string passwordSalt { get; set; }
    [FirestoreProperty]
    public string teacherEmail { get; set; }
    [FirestoreProperty]
    public string studentClass { get; set; }
    [FirestoreProperty]
    public string studentSchoolYear { get; set; }
    [FirestoreProperty]
    public string studentPoints { get; set; }
}
