using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    private float elapsedTime;
    private string stringTime;
    private bool isRunning = false;

    void Update()
    {
        SetTimer();
    }
    public void SetTimer()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            stringTime = string.Format("{0:00}:{1:00}", minutes, seconds);
            timer.text = stringTime;
        }
    }

    void OnEnable()
    {
        StartTimer();
    }

    void OnDisable()
    {
        StopTimer();
    }

    public void StartTimer()
    {
        if (!isRunning)
        {
            isRunning = true;
           // startTime = Time.time;
            Debug.Log(elapsedTime);
            Debug.Log(stringTime);
        }
    }
    public void StopTimer()
    {
        if (isRunning)
        {
            isRunning = false;
            //stopTime = Time.time;
            Debug.Log(elapsedTime);
            Debug.Log(stringTime);
        }
    }
    public void ResetTimer()
    {

    }
}
