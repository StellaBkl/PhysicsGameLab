using Firebase.Extensions;
using Firebase.Firestore;
using SojaExiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using Vuforia;
using static UnityEngine.GraphicsBuffer;

public class HandleExerciseActions : MonoBehaviour
{
    public TextMeshProUGUI ExerciseId;
    public TextMeshProUGUI QuizId;
    public TextMeshProUGUI UserId;
    public GameObject GamesCanvas;
    public GameObject AppCanvas;
    public GameObject QuizCanvas;
    public GameObject Game0022Prefab;

    private Quiz currentQuiz;
    private Exercise[] currentExercises;

    FirebaseFirestore dbFirestore;

    private void Start()
    {
        dbFirestore = FirebaseFirestore.DefaultInstance;
    }

    public GameObject GetGames(string gameId)
    {
        Transform game = GamesCanvas.transform.Find("Game" + gameId);

        if (game != null)
        {
            ExerciseId.text = gameId;
            return game.gameObject;
        }

        return null;
    }

    public void CreateQuizPrefab(string exerciseId, bool activateInstance, GameObject currentInstance)
    {
        string prefabPath = "GamePrefabs/Game" + exerciseId;
        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        GameObject quizTemplate = QuizCanvas.transform.Find("GameTemplate").gameObject;
        
        if (prefab != null)
        {
            //Unregister the old target
            StartCoroutine(ResetVuforiaTracking());

            if (currentInstance != null) Destroy(currentInstance);

            // Wait before instantiating a new target
            StartCoroutine(InstantiateNewPrefab(currentInstance, activateInstance, prefab, quizTemplate));

            //GameObject newInstance;
            //newInstance = Instantiate(prefab, quizTemplate.transform.position, quizTemplate.transform.rotation);
            //newInstance.transform.SetParent(quizTemplate.transform, false);
            //newInstance.transform.localPosition = Vector3.zero;
            //newInstance.transform.localScale = Vector3.one;
            //newInstance.name = prefab.name;
            //newInstance.SetActive(activateInstance);

            //currentInstance = newInstance;
        }
    }
    private IEnumerator InstantiateNewPrefab(GameObject currentInstance, bool activateInstance, GameObject prefab, GameObject quizTemplate)
    {
        yield return new WaitForSeconds(0.6f); // Ensure Vuforia is fully reset

        // Instantiate new Image Target prefab
        currentInstance = Instantiate(prefab, quizTemplate.transform.position, quizTemplate.transform.rotation);
        currentInstance.transform.SetParent(quizTemplate.transform, false);
        currentInstance.transform.localPosition = Vector3.zero;
        currentInstance.transform.localScale = Vector3.one;
        currentInstance.name = prefab.name;
        currentInstance.SetActive(activateInstance);

        // Ensure Vuforia starts tracking the new instance
        StartCoroutine(ResetVuforiaTracking());
        Debug.Log("New target instantiated and Vuforia tracking reset.");
    }
    public IEnumerator ResetVuforiaTracking()
    {
        if (VuforiaBehaviour.Instance != null)
        {
            VuforiaBehaviour.Instance.enabled = false; // Disable Vuforia
            yield return new WaitForSeconds(0.5f); // Small delay before re-enabling
            VuforiaBehaviour.Instance.enabled = true; // Re-enable Vuforia
            Debug.Log("Vuforia tracking reset.");
        }
    }
    public void OnOpenQuizClick(string quizId)
    {
        Debug.Log(quizId);
        Firebase.Firestore.Query query = dbFirestore.Collection("Quizzes").WhereEqualTo("quizId", quizId);
        query.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            var snapshot = task.Result;

            if (snapshot == null) return;

            foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
                Quiz quiz = documentSnapshot.ConvertTo<Quiz>();
                currentQuiz = quiz;
                currentExercises = quiz.chapter.exercises;

                if (currentExercises != null)
                {
                    ExerciseId.text = currentExercises[0].exerciseId;
                    CreateQuizPrefab(currentExercises[0].exerciseId, true, null);

                    if (AppCanvas != null)
                    {
                        AppCanvas.SetActive(false);
                    }
                    if (QuizCanvas != null)
                    {
                        GameObject quizTemplate = QuizCanvas.transform.Find("GameTemplate").gameObject;
                        quizTemplate.SetActive(true);
                        QuizCanvas.SetActive(true);
                    }
                }
                break;

            }
        });
    }
    public void OnNextExerciseClick()
    {
        List<Exercise> exercisesList = new List<Exercise>(currentExercises);
        exercisesList.RemoveAll(ex => ex.exerciseId.Equals(ExerciseId.text));

        GameObject quizTemplate = QuizCanvas.transform.Find("GameTemplate").gameObject;
        Transform game = quizTemplate.transform.Find("Game" + ExerciseId.text);

        if (exercisesList.Count == 0) 
        {
            if (game != null)
            {
                Destroy(game.gameObject);
            }
            quizTemplate.SetActive(false);
            //Debug.Log(currentQuiz.chapter.chapterId);
            EndOfQuiz();
            return;
        }

        currentExercises = exercisesList.ToArray();

        ExerciseId.text = currentExercises[0].exerciseId;
        CreateQuizPrefab(currentExercises[0].exerciseId, true, game.gameObject);

        if (AppCanvas != null)
        {
            AppCanvas.SetActive(false);
        }
        if (QuizCanvas != null)
        {
            QuizCanvas.SetActive(true);
        }
    }
    public void EndOfQuiz() 
    {
        GameObject quizDialogs = QuizCanvas.transform.Find("QuizDialogs").gameObject;

        if (quizDialogs != null) 
        { 
            quizDialogs.SetActive(true);
            float totalGrade = 0;
            int totalPoints = 0;

            for (int i = 0; i < currentQuiz.chapter.exercises.Length; i++)
            {
                Exercise exercise = currentQuiz.chapter.exercises[i];

                totalPoints += int.Parse(exercise.exercisePoints);
                totalGrade += exercise.grade;
            }
            float average = totalGrade / currentQuiz.chapter.exercises.Length;

            if (average < 5)
            {
                totalPoints = 0;
            }

            GameObject gameOverDialog = quizDialogs.transform.Find("GameOverDialog").gameObject;
            GameObject successDialog = quizDialogs.transform.Find("SuccessDialog").gameObject;

            if (average >= 5)
            {
                TextMeshProUGUI pointsText = successDialog.transform.Find("Background/PointsContainer/Points").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI gradeText = successDialog.transform.Find("Background/GradeContainer/Grade").GetComponent<TextMeshProUGUI>();
                pointsText.text = totalPoints.ToString();
                gradeText.text = average.ToString("F2");
                successDialog.SetActive(true); 
            }
            else
            {
                TextMeshProUGUI pointsText = gameOverDialog.transform.Find("Background/PointsContainer/Points").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI gradeText = gameOverDialog.transform.Find("Background/GradeContainer/Grade").GetComponent<TextMeshProUGUI>();
                pointsText.text = totalPoints.ToString();
                gradeText.text = average.ToString("F2");
                gameOverDialog.SetActive(true);
            }
            average = Mathf.Round(average * 10f) / 10f;

            CreateGradeItem(average, totalPoints);
        }
    }
    public void OnQuitExQuizClick()
    {
        GameObject quizTemplate = QuizCanvas.transform.Find("GameTemplate").gameObject;
        Transform game = quizTemplate.transform.Find("Game" + ExerciseId.text);
        //Debug.Log(game);
        if (game != null)
        {
            Destroy(game.gameObject);
        }
        if (AppCanvas != null)
        {
            AppCanvas.SetActive(true);
        }
        if (QuizCanvas != null)
        {
            QuizCanvas.SetActive(false);
        }

    }
    public void OnSubmitExClick(float grade, int points)
    {
        UpdateQuizData(grade, points);
    }
    private void UpdateQuizData(float grade, int points)
    {
        for (int i = 0; i < currentQuiz.chapter.exercises.Length; i++)
        {
            Exercise exercise = currentQuiz.chapter.exercises[i];

            if (exercise.exerciseId.Equals(ExerciseId.text))
            {
                exercise.exercisePoints = points.ToString();
                exercise.grade = grade;
                currentQuiz.chapter.exercises[i] = exercise;
            }
        }
    }
    public void OnSubmitQuizClick(float grade, int points)
    {
        //CreateGradeItem(grade, points);
    }
    private void CreateGradeItem(float grade, int points)
    {
        if (UserId.text != "")
        {
            Firebase.Firestore.Query queryS = dbFirestore.Collection("Student").WhereEqualTo("studentId", UserId.text);
            queryS.GetSnapshotAsync().ContinueWithOnMainThread(task => 
            {
                var snapshot = task.Result;
                Student student = new Student();
                string documentId = "";

                if (snapshot == null) return;

                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                {
                    student = documentSnapshot.ConvertTo<Student>();
                    documentId = documentSnapshot.Id;
                    break;
                }

                student.studentPoints = (int.Parse(student.studentPoints) + points).ToString();
                UpdateStudentData(student, documentId);
                currentQuiz.quizPoints = points.ToString();

                Grades newGrade = new Grades
                {
                    createdAt = DateTime.Now,
                    grade = grade,
                    student = student,
                    quiz = currentQuiz
                };

                Firebase.Firestore.CollectionReference gradeRef = dbFirestore.Collection("Grades");
                gradeRef.AddAsync(newGrade);

                //Firebase.Firestore.Query queryQ = dbFirestore.Collection("Quizzes").WhereEqualTo("quizId", QuizId.text);
                //queryQ.GetSnapshotAsync().ContinueWithOnMainThread(task =>
                //{
                //    var snapshot = task.Result;
                //    Quiz quiz = new Quiz();

                //    if (snapshot == null) return;

                //    foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                //    {
                //        quiz = documentSnapshot.ConvertTo<Quiz>();
                //        break;
                //    }
                //});

            });
        }
    }



    public void OnOpenExerciseClick(string exId)
    {
        Debug.Log(exId);
        GameObject playingGame = GetGames(exId);

        if (playingGame != null)
        {
            playingGame.SetActive(true);

            if (AppCanvas != null)
            {
                AppCanvas.SetActive(false);
            }
        }


    }
    public void OnQuitExerciseClick()
    {
        GameObject playingGame = GetGames(ExerciseId.text);

        if (playingGame != null)
        {
            ResetToPrefab(playingGame, false);

            if (AppCanvas != null)
            {
                AppCanvas.SetActive(true);
            }
        }
    }
    public void OnResetExerciseClick()
    {
        GameObject playingGame = GetGames(ExerciseId.text);

        if (playingGame != null)
        {
            ResetToPrefab(playingGame, true);
            playingGame.SetActive(true);
        }
    }

    // Resets the object by destroying the current instance and instantiating a new one
    public void ResetToPrefab(GameObject currentInstance, bool activateInstance)
    {
        string instanceName = currentInstance.name;

        Destroy(currentInstance);

        currentInstance = Instantiate(Game0022Prefab, GamesCanvas.transform.position, GamesCanvas.transform.rotation);
        currentInstance.transform.SetParent(GamesCanvas.transform, false);
        currentInstance.transform.localPosition = Vector3.zero;
        currentInstance.transform.localScale = Vector3.one;
        currentInstance.name = instanceName;
        currentInstance.SetActive(activateInstance);
    }

    public void OnSubmitExerciseClick(float grade, int points)
    {
        Debug.Log(grade + " "+ points);
        GetChaptersAndExercises(grade, points);
    }

    private void GetChaptersAndExercises(float grade, int points)
    {
        Firebase.Firestore.Query query = dbFirestore.Collection("Chapters");
        query.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            var snapshot = task.Result;

            if (snapshot == null) return;

            foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
            
                Chapters chapter = documentSnapshot.ConvertTo<Chapters>();


                if (UserId.text != "")
                {
                    Firebase.Firestore.Query query = dbFirestore.Collection("Student");
                    query.GetSnapshotAsync().ContinueWithOnMainThread(task =>
                    {
                        var snapshot = task.Result;
                        Student student = new Student();
                        string documentId = "";

                        if (snapshot == null) return;

                        foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                        {
                            Student st = documentSnapshot.ConvertTo<Student>();

                            if (UserId.text == st.studentId)
                            {
                                documentId = documentSnapshot.Id;
                                student = st;
                                break;
                            }
                        }

                        for (int i = 0; i < chapter.exercises.Length; i++)
                        {
                            Exercise exercise = chapter.exercises[i];

                            if (ExerciseId.text == exercise.exerciseId)
                            {
                                exercise.exercisePoints = points.ToString();
                                student.studentPoints = (int.Parse(student.studentPoints) + points).ToString();
                                UpdateStudentData(student, documentId);

                                Quiz newQuiz = new Quiz
                                {
                                    quizId = "24878",
                                    quizName = "Quiz 1",
                                    teacherEmail = student.teacherEmail,
                                    createdAt = DateTime.Now,
                                    chapter = chapter
                                };

                                Grades newGrade = new Grades
                                {
                                    createdAt = DateTime.Now,
                                    grade = grade,
                                    student = student,
                                    quiz = newQuiz
                                };

                                Firebase.Firestore.CollectionReference gradeRef = dbFirestore.Collection("Grades");
                                gradeRef.AddAsync(newGrade);
                                break;
                            }
                        }

                    });
                }
            }
        });
    }

    private void UpdateStudentData(Student student, string documentId)
    {
        Dictionary<string, object> updates = new Dictionary<string, object>
        {
            { "studentPoints", student.studentPoints }
        };

        Firebase.Firestore.DocumentReference studentRef = dbFirestore.Collection("Student").Document(documentId);
        studentRef.UpdateAsync(updates);

    }
}
