using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private HandleExerciseActions exerciseAction;

    public void QuitExerciseGame()
    {
        exerciseAction = FindObjectOfType<HandleExerciseActions>();
        exerciseAction.OnQuitExerciseClick();
    } 
    
    public void ResetExerciseGame()
    {
        exerciseAction = FindObjectOfType<HandleExerciseActions>();
        exerciseAction.OnResetExerciseClick();
    }
}
