using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ChapterPage : MonoBehaviour
{
    public TextMeshProUGUI UserId;

    public GameObject ChaptersPrefab;
    public float verticalSpacingC = 1063.129f;
    public GameObject ExercisesPrefab;
    public float verticalSpacingE = 295.561f;
    public GameObject QuizzesPrefab;
    public float verticalSpacingQ = 295.561f;

    FirebaseFirestore dbFirestore;

    private void Start()
    {
        dbFirestore = FirebaseFirestore.DefaultInstance;

        GetQuizzes();
    }

    private void GetQuizzes()
    {
        Firebase.Firestore.Query query = dbFirestore.Collection("Quizzes").OrderByDescending("createdAt");
        query.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            var snapshot = task.Result;
            Dictionary<int, float> progressByChapter = new Dictionary<int, float>();

            if (snapshot == null) return;

            Vector3 quizSpawnPosition = Vector3.zero;
            int i = 1;

            foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
                Quiz quiz = documentSnapshot.ConvertTo<Quiz>();

                if (!quiz.isEnabled) continue;

                Chapters chapter = quiz.chapter;
                long totalExercises = chapter.exercises.LongCount<Exercise>();

                HashSet<int> completedExercises = new HashSet<int>();
                GameObject quizObject;

                if (quizSpawnPosition != Vector3.zero)
                {
                    quizObject = Instantiate(QuizzesPrefab, quizSpawnPosition, Quaternion.identity, transform);
                }
                else
                {
                    quizObject = Instantiate(QuizzesPrefab, transform);
                }

                TextMeshProUGUI quizId = quizObject.transform.Find("QuizId").GetComponent<TextMeshProUGUI>();
                UnityEngine.UI.Image quizImg = quizObject.transform.Find("OptionPhoto").GetComponent<UnityEngine.UI.Image>();
                Transform titlePanel = quizObject.transform.Find("OptionTitle");

                //TODO change to quiz id
                quizId.text = quiz.quizId;

                string imgUrl = "ExerciseImgs/" + chapter.exercises[0].exerciseImg;
                Sprite quizSprite = Resources.Load<Sprite>(imgUrl);

                if (quizSprite != null)
                {
                    quizImg.sprite = quizSprite;
                }

                if (titlePanel != null)
                {
                    TextMeshProUGUI quizTitle = titlePanel.transform.Find("QuizTitle").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI chapterTitle = titlePanel.transform.Find("ChapterTitle").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI points = titlePanel.transform.Find("Points").GetComponent<TextMeshProUGUI>();


                    quizTitle.text = quiz.quizName + " - Περιέχει "+ totalExercises + " ασκήσεις";
                    chapterTitle.text = chapter.chapterId +" - "+ chapter.chapterName;
                    points.text = quiz.quizPoints;
                }

                quizSpawnPosition = quizObject.transform.position;
                quizSpawnPosition.y -= verticalSpacingC;
                i++;
            }

        });
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

                                if (UserId.text == grade.student.studentId && grade.quiz.chapter.chapterId == chapter.chapterId)
                                {
                                    foreach (Exercise ex in chapter.exercises)
                                    {
                                        //if(ex.exerciseId == grade.exercise.exerciseId &&
                                        //    grade.grade>=5)
                                        //{
                                        //    completedExercises.Add(int.Parse(grade.exercise.exerciseId));
                                        //}
                                    }
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
