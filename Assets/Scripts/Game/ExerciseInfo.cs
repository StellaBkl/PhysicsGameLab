using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class ExerciseInfo : MonoBehaviour
{
    FirebaseFirestore dbFirestore;

    private void Start()
    {
        dbFirestore = FirebaseFirestore.DefaultInstance;
        
        GetExerciseInfo();
    }

    public void GetExerciseInfo()
    {

        string gameName = gameObject.name;

        Firebase.Firestore.Query query = dbFirestore.Collection("Chapters");
        query.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            var snapshot = task.Result;

            if (snapshot == null) return;

            foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
                Chapters chapter = documentSnapshot.ConvertTo<Chapters>();

                foreach (Exercise exercise in chapter.exercises)
                {
                    string gameNumber = Regex.Match(gameName, @"\d+").Value;

                    if (gameNumber == exercise.exerciseId)
                    {
                        Transform startPanel = gameObject.transform.Find("GameCanvas/StartPanel/Background");
                        
                        if (startPanel != null)
                        {
                            TextMeshProUGUI points = startPanel.transform.Find("PointsContainer/Points").GetComponent<TextMeshProUGUI>();

                            if (points != null)
                            {
                                points.text = exercise.exercisePoints.ToString();
                            }
                        }

                    }
                }
            }

        });
    }
}
