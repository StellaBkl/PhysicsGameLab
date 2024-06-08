using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ExerciseSelect : MonoBehaviour
{
    public Canvas exerciseObject;
    private GamePages games;

    void Start()
    {
    }
    public void OnExerciseClick(TextMeshProUGUI exerciseId)
    {
        Debug.Log(exerciseId.text);

        games = FindObjectOfType<GamePages>();
        GameObject playingGame = games.GetGames(exerciseId.text);

        if (playingGame != null)
        {
            playingGame.SetActive(true);
        }
        else
        {
            Debug.LogError("exerciseObject not found");
        }

        GameObject exerciseObject = GameObject.Find("AppCanvas");

        if (exerciseObject != null)
        {
            exerciseObject.SetActive(false);
        }
        else
        {
            Debug.LogError("exerciseObject not found");
        }
    }
}
