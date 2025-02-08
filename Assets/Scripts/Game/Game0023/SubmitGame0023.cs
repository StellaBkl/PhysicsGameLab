using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SubmitGame0023 : MonoBehaviour
{
    public GameObject pointsObject;

    private List<string> correctStrings = new List<string>
    {
        "A-Top","B-Bottom","C-Left","D-Bottom"
    };
    private const float correctGrade = 2.5f;
    private const float incorrectPenalty = -1f;

    public void OnGameSubmit()
    {
        Debug.Log("Submit Game 0023");

        float grade = 0f;
        foreach (Transform selectionItem in pointsObject.transform)
        {
            Transform selection = selectionItem.Find("SelectedPoint");

            if (selection != null) 
            { 
                TextMeshProUGUI selectionText = selection.gameObject.GetComponent<TextMeshProUGUI>();
                Debug.Log(selectionText.text);

                if (selectionText.text != "")
                {
                    if (correctStrings.Contains(selectionText.text))
                    {
                        grade += correctGrade;
                    }
                    else
                    {
                        grade += incorrectPenalty;
                    }
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
