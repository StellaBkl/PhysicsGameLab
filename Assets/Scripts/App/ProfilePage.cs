using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfilePage : MonoBehaviour
{
    public TextMeshProUGUI UserId;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Surname;
    public TextMeshProUGUI Avatar;
    public TextMeshProUGUI Points;
    public TextMeshProUGUI Username;

    private Student student;

    FirebaseFirestore dbFirestore;

    private void Start()
    {
        //dbFirestore = FirebaseFirestore.DefaultInstance;

        //Debug.Log(UserId.text);
        //if (UserId != null && UserId.text != "")
        //{
        //    GetCurrentStudent();
        //}
    }

    // Run Firebase query when GameObject is activated
    void OnEnable()
    {
        dbFirestore = FirebaseFirestore.DefaultInstance;

        if (UserId != null && UserId.text != "")
        {
            GetCurrentStudent();
        }
    }

    private void GetCurrentStudent()
    {
        Firebase.Firestore.Query query = dbFirestore.Collection("Student").WhereEqualTo("studentId", UserId.text);
        query
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task => {
                var snapshot = task.Result;

                if (snapshot != null)
                {
                    foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                    {
                        student = documentSnapshot.ConvertTo<Student>();
                        Avatar.text = student.studentName.ToCharArray()[0].ToString() + student.studentSurname.ToCharArray()[0].ToString();
                        Name.text = student.studentName;
                        Surname.text = student.studentSurname;
                        Points.text = student.studentPoints;
                        Username.text = student.studentUsername;
                    }
                }
            });

    }
}
