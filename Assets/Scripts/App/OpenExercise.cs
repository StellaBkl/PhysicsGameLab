using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenExercise : MonoBehaviour
{
    private HandleExerciseActions exerciseAction;

    public void OpenExerciseGame(TextMeshProUGUI exerciseId)
    {
        exerciseAction = FindObjectOfType<HandleExerciseActions>();
        exerciseAction.OnOpenExerciseClick(exerciseId.text);
    }
}
