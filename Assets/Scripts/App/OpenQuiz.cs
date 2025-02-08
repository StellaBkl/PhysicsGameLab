using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenQuiz : MonoBehaviour
{
    private HandleExerciseActions exerciseAction;

    public void OpenCurrentQuiz(TextMeshProUGUI quizId)
    {
        exerciseAction = FindObjectOfType<HandleExerciseActions>();
        exerciseAction.OnOpenQuizClick(quizId.text);
    }
}
