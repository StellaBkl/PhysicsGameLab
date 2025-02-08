using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class HandleVuforiaTarget : MonoBehaviour
{
    private DefaultObserverEventHandler defaultHandler;
    private ObserverBehaviour observer;
    void Start()
    {
        // Dynamically add the DefaultObserverEventHandler component
        defaultHandler = gameObject.AddComponent<DefaultObserverEventHandler>();

        // DefaultObserverEventHandler will now handle target found/lost behaviors
        Debug.Log("DefaultObserverEventHandler added and initialized.");

        observer = GetComponent<ObserverBehaviour>();
        if (observer != null)
        {
            observer.enabled = true;
            observer.OnTargetStatusChanged += HandleTargetStatusChanged; 
        }
    }

    private void HandleTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        Debug.Log($"Target status: {status.Status} -- {status.StatusInfo}");

        if (status.Status == Status.TRACKED)
        {
            OnTargetFound();
        }
        else if (status.Status == Status.NO_POSE)
        {
            OnTargetLost();
        }
    }

    private void OnTargetFound()
    {
        Debug.Log("Target Found!");
        // Add logic when target is found
    }

    private void OnTargetLost()
    {
        Debug.Log("Target Lost!");
        // Add logic when target is lost
    }

    void OnDestroy()
    {
        if (observer != null)
        {
            observer.OnTargetStatusChanged -= HandleTargetStatusChanged;
            Debug.Log("Destroy target");
            observer.enabled = false;
        }
    }
}
