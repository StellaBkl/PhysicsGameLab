using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubmitExercise : MonoBehaviour
{
    private HandleExerciseActions exerciseAction;
    private int standardSeconds = 30; //secs to count points


    public void HandleGradeAndPoints(Transform dialogPanel, float correctItemsCount)
    {
        dialogPanel.gameObject.SetActive(true);

        TextMeshProUGUI grade = dialogPanel.transform.Find("Background/GradeContainer/Grade").GetComponent<TextMeshProUGUI>();
        Transform gainedPointsContainer = dialogPanel.transform.Find("Background/PointsContainer");
        Transform pointsContainer = gameObject.transform.Find("GameCanvas/StartPanel/Background/PointsContainer");

        if (grade != null)
        {
            grade.text = correctItemsCount.ToString("F2") + "/10";

            int totalPoints = 0;

            if (pointsContainer != null)
            {
                TextMeshProUGUI points = pointsContainer.transform.Find("Points").GetComponent<TextMeshProUGUI>();

                if (correctItemsCount >= 5)
                {
                    totalPoints = CalculatePoints(int.Parse(points.text));
                }

                if (gainedPointsContainer != null)
                {
                    TextMeshProUGUI gainedPoints = gainedPointsContainer.transform.Find("Points").GetComponent<TextMeshProUGUI>();

                    gainedPoints.text = totalPoints + "/" + points.text;
                }

                exerciseAction = FindObjectOfType<HandleExerciseActions>();
                exerciseAction.OnSubmitExClick(correctItemsCount, totalPoints);
            }
        }
    }

    private int CalculatePoints(int points)
    {
        TextMeshProUGUI timer = gameObject.transform.Find("GameCanvas/Timer").GetComponent<TextMeshProUGUI>();

        string[] timeParts = timer.text.Split(':');

        int minutes = int.Parse(timeParts[0]);
        int seconds = int.Parse(timeParts[1]);

        int totalSeconds = (minutes * 60) + seconds;

        //starts deducting points after 30secs
        if (totalSeconds > standardSeconds)
        {
            float pointsSelected = (totalSeconds - standardSeconds) * 0.5f;
            int totalPointsToDeduct = Mathf.FloorToInt(pointsSelected);
            //int totalPointsToDeduct = (totalSeconds - standardSeconds) * 2;

            points -= totalPointsToDeduct;
        }
        points = Mathf.Max(points, 20);

        return points;
    }
}
