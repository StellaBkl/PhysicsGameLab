using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SelectItem : MonoBehaviour
{
    public GameObject currentSelection;
    private HandleExerciseSelection exerciseSelection;

    void OnMouseDown()
    {
        exerciseSelection = FindObjectOfType<HandleExerciseSelection>();
        exerciseSelection.SelectCircuit(currentSelection);
    }
}
