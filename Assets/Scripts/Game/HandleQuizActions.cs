using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandleQuizActions : MonoBehaviour
{
    private HandleExerciseActions exerciseAction;
    private int standardSeconds = 30; //secs to count points

    public void QuitExerciseGame()
    {
        exerciseAction = FindObjectOfType<HandleExerciseActions>();
        exerciseAction.OnQuitExQuizClick();
    }

    public void QuitQuiz()
    {
        exerciseAction = FindObjectOfType<HandleExerciseActions>();
        exerciseAction.OnQuitExQuizClick();
    }

    public void NextExercisGame()
    {
        exerciseAction = FindObjectOfType<HandleExerciseActions>();
        exerciseAction.OnNextExerciseClick();
    }
    public void SubmitQuiz()
    {
        exerciseAction = FindObjectOfType<HandleExerciseActions>();
        //exerciseAction.OnSubmitQuizClick();
    }


    public void HandleGradeAndPoints(Transform dialogPanel, float correctItemsCount)
    {
        dialogPanel.gameObject.SetActive(true);

        TextMeshProUGUI grade = dialogPanel.transform.Find("Background/GradeContainer/Grade").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI gainedPoints = dialogPanel.transform.Find("Background/PointsContainer/Points").GetComponent<TextMeshProUGUI>();
        Debug.Log(gainedPoints);
        TextMeshProUGUI points = gameObject.transform.Find("StartPanel/Background/PointsContainer/Points").GetComponent<TextMeshProUGUI>();

        Debug.Log(grade);
        Debug.Log(correctItemsCount);
        if (grade != null)
        {
            grade.text = correctItemsCount + "/10";

            int totalPoints = 0;
            if (correctItemsCount >= 5)
            {
                totalPoints = CalculatePoints(int.Parse(points.text));
            }

            if (gainedPoints != null)
            {
                gainedPoints.text = totalPoints + "/" + points.text;
            }

            exerciseAction = FindObjectOfType<HandleExerciseActions>();
            exerciseAction.OnSubmitExerciseClick(correctItemsCount, totalPoints);
        }
    }

    private int CalculatePoints(int points)
    {
        TextMeshProUGUI timer = gameObject.transform.Find("Timer").GetComponent<TextMeshProUGUI>();

        string[] timeParts = timer.text.Split(':');

        int minutes = int.Parse(timeParts[0]);
        int seconds = int.Parse(timeParts[1]);

        int totalSeconds = (minutes * 60) + seconds;

        //starts deducting points after 30secs
        if (totalSeconds > standardSeconds)
        {
            int totalPointsToDeduct = (totalSeconds - standardSeconds) * 2;

            points -= totalPointsToDeduct;
        }
        points = Mathf.Max(points, 10);

        return points;
    }
}
