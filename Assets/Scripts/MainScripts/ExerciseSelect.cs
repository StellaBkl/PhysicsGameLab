using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExerciseSelect : MonoBehaviour
{
    //public GameObject Game1;

    void Start()
    {
        Debug.Log("ex");
    }
    public void OnExerciseClick(TextMeshProUGUI exerciseId)
    {
        Debug.Log(exerciseId.text);
        //GameObject gameObject = new GameObject();
        //gameObject.Find("Game1");
    }
}
