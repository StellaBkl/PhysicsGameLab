using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ChapterPage : MonoBehaviour
{
    public TextMeshProUGUI UserId;

    public GameObject ChaptersPrefab;
    public float verticalSpacingC = 1063.129f;
    public GameObject ExercisesPrefab;
    public float verticalSpacingE = 295.561f;

    FirebaseFirestore dbFirestore;

    private void Start()
    {
        dbFirestore = FirebaseFirestore.DefaultInstance;

        GetChaptersAndExercises();
    }

    private void GetChaptersAndExercises()
    {
        Firebase.Firestore.Query query = dbFirestore.Collection("Chapters");
        query.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            var snapshot = task.Result;
            Dictionary<int, float> progressByChapter = new Dictionary<int, float>();

            if (snapshot == null) return;

            Vector3 chapterSpawnPosition = Vector3.zero;

            foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
                Chapters chapter = documentSnapshot.ConvertTo<Chapters>();
                long totalExercises = chapter.exercises.LongCount<Exercise>();
                HashSet<int> completedExercises = new HashSet<int>();
                GameObject chapterObject;

                if (chapterSpawnPosition != Vector3.zero)
                {
                    chapterObject = Instantiate(ChaptersPrefab, chapterSpawnPosition, Quaternion.identity, transform);
                }
                else
                {
                    chapterObject = Instantiate(ChaptersPrefab, transform);
                }

                Transform titlePanel = chapterObject.transform.Find("TitlePanel");
                TextMeshProUGUI chName = titlePanel.transform.Find("ChapterName").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI chId = chapterObject.transform.Find("ChapterId").GetComponent<TextMeshProUGUI>();

                Transform progressBarObj = chapterObject.transform.Find("ProgressBar");
                UnityEngine.UI.Slider progressBar = progressBarObj.GetComponent<UnityEngine.UI.Slider>();
                TextMeshProUGUI percentage = progressBarObj.transform.Find("Percentage").GetComponent<TextMeshProUGUI>();

                chName.text = chapter.chapterName;
                chId.text = chapter.chapterId;

                if (UserId.text != "")
                {
                    Firebase.Firestore.Query query = dbFirestore.Collection("Grades");
                    query.GetSnapshotAsync().ContinueWithOnMainThread(task =>
                    {
                        var snapshot = task.Result;

                        if (snapshot == null) return;
                        
                            foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                            {
                                Grades grade = documentSnapshot.ConvertTo<Grades>();

                                if (UserId.text == grade.student.studentId && grade.chapterId == chapter.chapterId 
                                    && chapter.exercises.Contains(grade.exercise))
                                {
                                    completedExercises.Add(int.Parse(grade.exercise.exerciseId));
                                }

                            float progressPercentage = (float)completedExercises.Count / totalExercises * 100;

                            progressByChapter[int.Parse(chapter.chapterId)] = progressPercentage;
                            progressBar.value = progressPercentage;
                            percentage.text = $"{progressPercentage:F1}%";
                        }
                        
                    });
                }

                Transform optionsPanel = chapterObject.transform.Find("OptionsPanel");
                Transform exercisesContainer = optionsPanel.transform.Find("ExerciseContent");

                Vector3 exerciseSpawnPosition = Vector3.zero;

                foreach (Exercise exercise in chapter.exercises)
                {
                    GameObject exerciseObject = Instantiate(ExercisesPrefab, exercisesContainer);

                    Transform titlePanelEx = exerciseObject.transform.Find("OptionTitle");
                    TextMeshProUGUI exId = exerciseObject.transform.Find("ExerciseId").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI exTitle = titlePanelEx.transform.Find("Title").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI exPoints = titlePanelEx.transform.Find("Points").GetComponent<TextMeshProUGUI>();

                    exTitle.text = exercise.exerciseTitle;
                    exId.text = exercise.exerciseId;
                    exPoints.text = exercise.exercisePoints;

                    exerciseObject.transform.localPosition = exerciseSpawnPosition;
                    exerciseSpawnPosition.y -= verticalSpacingE;
                }

                chapterSpawnPosition = chapterObject.transform.position;
                chapterSpawnPosition.y -= verticalSpacingC;
            }

        });
    }
}
