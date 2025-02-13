using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubmitGame0013 : MonoBehaviour
{
    public TextMeshProUGUI selectedItems;

    private const float correctGrade = 3.34f;
    private const float incorrectPenalty = 1.1f;
    public void OnGameSubmit()
    {
        Debug.Log("Submit Game 0013");
        string submitedItems = selectedItems.text;
        float grade = 0f;

        if (submitedItems.Contains("4")) grade += correctGrade;
        if (submitedItems.Contains("5")) grade += correctGrade;
        if (submitedItems.Contains("7")) grade += correctGrade;

        string result = submitedItems.Replace("4", "").Trim();
        result = result.Replace("5", "").Trim();
        result = result.Replace("7", "").Trim();

        char[] selectedI = result.ToCharArray();

        if (selectedI.Length > 0)
        {
            grade = grade - (selectedI.Length * incorrectPenalty);
        }

        grade = Mathf.Clamp(grade, 0f, 10f);

        ShowProgressDialog(grade);
    }

    private void ShowProgressDialog(float correctItemsCount)
    {
        Transform dialogPanel = null;

        if (correctItemsCount >= 5)
        {
            dialogPanel = gameObject.transform.Find("SuccessPanel");
        }
        else
        {
            dialogPanel = gameObject.transform.Find("GameOverPanel");
        }

        if (dialogPanel != null)
        {
            SubmitExercise submitExercise = FindObjectOfType<SubmitExercise>();
            submitExercise.HandleGradeAndPoints(dialogPanel, correctItemsCount);
        }
    }
}
