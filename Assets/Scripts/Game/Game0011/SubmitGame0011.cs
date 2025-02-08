using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitGame0011 : MonoBehaviour
{
    private const float incorrectPenalty = 0.7f;

    public void OnGameSubmit()
    {
        Debug.Log("Submit Game 0011");
        Transform gameTemplate = gameObject.transform.Find("GameTemplate");
        Transform inventoryPanel = gameObject.transform.Find("InventoryPanel");

        Transform conductors = gameTemplate != null ? gameTemplate.gameObject.transform.Find("Conductors") : null;
        Transform insulators = gameTemplate != null ? gameTemplate.gameObject.transform.Find("Insulators") : null;

        Transform conCorrectItems = conductors != null ? conductors.gameObject.transform.Find("CorrectItems") : null;
        Transform inCorrectItems = insulators != null ? insulators.gameObject.transform.Find("CorrectItems") : null;
        
        Transform conWrongItems = conductors != null ? conductors.gameObject.transform.Find("OtherItems") : null;
        Transform inWrongItems = insulators != null ? insulators.gameObject.transform.Find("OtherItems") : null;

        List<GameObject> correctItems = new List<GameObject>();
        List<GameObject> wrongItems = new List<GameObject>();

        if (conCorrectItems != null)
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

        if (conWrongItems != null)
        {
            foreach (Transform child in conWrongItems.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    wrongItems.Add(child.gameObject);
                }
            }
        }

        if (inWrongItems != null)
        {
            foreach (Transform child in inWrongItems.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    wrongItems.Add(child.gameObject);
                }
            }
        }

        float grade = correctItems.Count;

        if (wrongItems.Count > 0)
        {
            grade = grade - (wrongItems.Count * incorrectPenalty);
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
