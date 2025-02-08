using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Firebase.Firestore;
using Firebase.Extensions;
using Unity.VisualScripting;
using System.Linq;

public class ScoreboardData : MonoBehaviour
{
    public GameObject gradesPrefab;
    public float verticalSpacing = 217.45f;
    FirebaseFirestore dbFirestore;
    void Start()
    {
        
        //dbFirestore = FirebaseFirestore.DefaultInstance;
        //getScoreboardData();

    }

    // Run Firebase query when GameObject is activated
    void OnEnable()
    {
        dbFirestore = FirebaseFirestore.DefaultInstance;
        ClearContent();
        getScoreboardData();
    }
    private void ClearContent()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void getScoreboardData()
    {
        Debug.Log("Scoreboard Data");
        Firebase.Firestore.Query query = dbFirestore.Collection("Student").OrderByDescending("studentPoints");
            query.GetSnapshotAsync().ContinueWithOnMainThread(task => {
                var snapshot = task.Result;
                int counter = 1;
                //errorText.text = "Δεν βρέθηκαν δεδομένα.";

                if (snapshot != null)
                {
                    Vector3 spawnPosition = Vector3.zero;
                    List<Student> students = snapshot.Documents.Select(doc => doc.ConvertTo<Student>()).ToList();
                    students.Sort((a, b) =>
                    {
                        var aKey = long.Parse(a.studentPoints);
                        var bKey = long.Parse(b.studentPoints);

                        return bKey.CompareTo(aKey);
                    });

                    foreach(Student student in students)
                    {
                        GameObject gradesObject;
                        if (spawnPosition != Vector3.zero)
                        {
                             gradesObject = Instantiate(gradesPrefab, spawnPosition, Quaternion.identity, transform);
                        }
                        else
                        {
                             gradesObject = Instantiate(gradesPrefab, transform);
                        }
                       

                        // Access TextMeshPro elements within the instantiated prefab
                        TextMeshProUGUI position = gradesObject.transform.Find("Position").GetComponent<TextMeshProUGUI>();
                        TextMeshProUGUI name = gradesObject.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                        TextMeshProUGUI points = gradesObject.transform.Find("Points").GetComponent<TextMeshProUGUI>();

                        // Fill TextMeshPro elements with data
                        position.text = counter.ToString();
                        name.text = student.studentName + " " + student.studentSurname;
                        points.text = student.studentPoints;

                        spawnPosition = gradesObject.transform.position;
                        spawnPosition.y -= verticalSpacing;
                        counter++;
                    };
                }
            });
        }

    public void getScoreboardData1()
    {

        Firebase.Firestore.Query query = dbFirestore.Collection("Grades").OrderByDescending("grade");
        query.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            var snapshot = task.Result;
            int counter = 1;
            //errorText.text = "Δεν βρέθηκαν δεδομένα.";

            if (snapshot != null)
            {
                Vector3 spawnPosition = Vector3.zero;
                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                {
                    Grades grades = documentSnapshot.ConvertTo<Grades>();
                    Student student = grades.student;

                    //errorText.text = "";
                    // Instantiate the student prefab
                    GameObject gradesObject;
                    if (spawnPosition != Vector3.zero)
                    {
                        gradesObject = Instantiate(gradesPrefab, spawnPosition, Quaternion.identity, transform);
                    }
                    else
                    {
                        gradesObject = Instantiate(gradesPrefab, transform);
                    }


                    // Access TextMeshPro elements within the instantiated prefab
                    TextMeshProUGUI position = gradesObject.transform.Find("Position").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI name = gradesObject.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI level = gradesObject.transform.Find("Level").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI points = gradesObject.transform.Find("Points").GetComponent<TextMeshProUGUI>();

                    // Fill TextMeshPro elements with data
                    position.text = counter.ToString();
                    name.text = student.studentName + " " + student.studentSurname;
                    level.text = "10";
                    points.text = grades.grade.ToString();

                    spawnPosition = gradesObject.transform.position;
                    spawnPosition.y -= verticalSpacing;
                    counter++;
                };
            }
        });
    }

}
