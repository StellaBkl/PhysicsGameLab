using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitGame0014 : MonoBehaviour
{
    private List<GameObject> correctItems = new List<GameObject>();
    private List<GameObject> wrongItems = new List<GameObject>();
    private const float correctGrade = 1.25f;
    private const float incorrectPenalty = 0.6f;

    public void OnGameSubmit()
    {
        Debug.Log("Submit Game 0014");
        Transform gameTemplate = gameObject.transform.Find("GameTemplate/Base");

        if (gameTemplate == null) return;


        foreach (Transform child in gameTemplate)
        {
            GameObject correctI = child.Find("CorrectItems")!=null? child.Find("CorrectItems").gameObject: null;
            GameObject otherI = child.Find("OtherItems")!=null? child.Find("OtherItems").gameObject: null;

            if (correctI != null)
            {
                GetCorrectWrongItems(correctI);
            }

            if (otherI != null)
            {
                GetCorrectWrongItems(otherI);
            }
        }

        float grade = correctItems.Count * correctGrade;

        if (wrongItems.Count > 0)
        {
            grade = grade - (wrongItems.Count * incorrectPenalty);
        }
        grade = Mathf.Clamp(grade, 0f, 10f);

        ShowProgressDialog(grade);
    }

    private void GetCorrectWrongItems(GameObject items)
    {

        foreach (Transform child in items.transform)
        {
            if (child.gameObject.activeSelf)
            {
                if (items.name.Equals("CorrectItems"))
                {
                    correctItems.Add(child.gameObject);
                }
                else
                {
                    wrongItems.Add(child.gameObject);
                }
            }
        }
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
