using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubmitGame0024 : MonoBehaviour
{
    private Dictionary<string, string> correctAnswers = new Dictionary<string, string>
    {
        { "BlackPaper", "Absorption" },
        { "Mirror", "Reflection" },
        { "Wood", "Absorption" },
        { "WhitePaper", "Diffusion" },
        { "RicePaper", "Diffusion" },
        { "ColoredGelatin", "Diffusion" }
    };

    private const float correctGrade = 1.7f; //1.67f
    private const float incorrectPenalty = -0.9f;

    public void OnGameSubmit()
    {
        Debug.Log("Submit Game 0024");

        Transform inventoryPanel = gameObject.transform.Find("InventoryPanel");
        float grade = 0f;
        int correctCount = 0;
        int incorrectCount = 0;

        // Iterate through all child TextMeshPro components under the parent
        foreach (Transform item in inventoryPanel)
        {
            GameObject inventoryItem = item.gameObject;
            string itemName = inventoryItem.name;
            TextMeshProUGUI answer = inventoryItem.transform.Find("Answer").GetComponent<TextMeshProUGUI>();

            if (answer.text != "" && correctAnswers.TryGetValue(itemName, out string correctAnswer))
            {
                Debug.Log(correctAnswer);
                if (answer.text == correctAnswer)
                {
                    grade += correctGrade;
                    correctCount++;
                }
                else
                {
                    grade += incorrectPenalty;
                    incorrectCount++;
                }
            }
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
