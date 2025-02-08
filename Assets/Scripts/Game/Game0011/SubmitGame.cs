using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubmitGame : MonoBehaviour
{
    private HandleExerciseActions exerciseAction;
    private int standardSeconds = 30; //secs to count points

    public void OnGameSubmit()
    {
        Transform gameTemplate = gameObject.transform.Find("GameTemplate");

        Transform conductors = gameTemplate != null ? gameTemplate.gameObject.transform.Find("Conductors"): null;
        Transform insulators = gameTemplate != null ? gameTemplate.gameObject.transform.Find("Insulators"): null;

        Transform conCorrectItems = conductors != null ? conductors.gameObject.transform.Find("CorrectItems"): null;
        Transform inCorrectItems = insulators != null ? insulators.gameObject.transform.Find("CorrectItems"): null;

        //Debug.Log(conductors.gameObject);
        //Debug.Log(insulators.gameObject);

        List<GameObject> correctItems = new List<GameObject>();

        if(conCorrectItems != null)
        {
            foreach (Transform child in conCorrectItems.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    correctItems.Add(child.gameObject);
                }
            }
        }

        if (inCorrectItems != null)
        {
            foreach (Transform child in inCorrectItems.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    correctItems.Add(child.gameObject);
                }
            }
        }

        ShowProgressDialog(correctItems.Count);
    }

    private void ShowProgressDialog(int correctItemsCount)
    {
        Transform dialogPanel = null;

        if(correctItemsCount >= 5)
        {
            dialogPanel = gameObject.transform.Find("SuccessPanel");
        }
        else
        {
            dialogPanel = gameObject.transform.Find("GameOverPanel");
        }

        if (dialogPanel != null)
        {
            HandleGradeAndPoints(dialogPanel, correctItemsCount);
        }
    }

    private void HandleGradeAndPoints(Transform dialogPanel, int correctItemsCount)
    {
        dialogPanel.gameObject.SetActive(true);

        TextMeshProUGUI grade = dialogPanel.transform.Find("Background/GradeContainer/Grade").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI gainedPoints = dialogPanel.transform.Find("Background/PointsContainer/Points").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI points = gameObject.transform.Find("StartPanel/Background/PointsContainer/Points").GetComponent<TextMeshProUGUI>();

        if (grade != null)
        {
            grade.text = correctItemsCount + "/10";

            int totalPoints = 0;
            if (correctItemsCount >=5)
            {
                totalPoints = CalculatePoints(int.Parse(points.text));
            }

            if(gainedPoints != null)
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
