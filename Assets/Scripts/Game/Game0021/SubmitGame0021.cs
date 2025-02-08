using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubmitGame0021 : MonoBehaviour
{
    public TextMeshProUGUI selectedItems;

    private char[] correctLetters = { 'Α', 'Δ', 'Ζ' };
    private float correctGradePerLetter = 3.35f;

    private char[] wrongLetters = { 'Β', 'Γ', 'Ε' };
    private float penaltyPerWrongLetter = -1.7f;

    public void OnGameSubmit()
    {
        Debug.Log("Submit Game 0021");

        // Split the target string into individual characters
        string[] letters = selectedItems.text.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

        float grade = 0f;

        // Check for correct letters
        foreach (char correct in correctLetters)
        {
            if (System.Array.Exists(letters, l => l == correct.ToString()))
            {
                grade += correctGradePerLetter;
            }
        }

        // Check for wrong letters
        foreach (char wrong in wrongLetters)
        {
            if (System.Array.Exists(letters, l => l == wrong.ToString()))
            {
                grade += penaltyPerWrongLetter;
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
