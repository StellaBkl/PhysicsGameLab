using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PluginInit : MonoBehaviour
{
    private const string PluginName = "com.arapps.unitylib.PluginInstance";
    private const string MainActivityName = "com.ARGames.PhysicsGames.MainActivity";
    AndroidJavaClass unityClass;
    AndroidJavaObject unityActivity;
    AndroidJavaObject _pluginInstance;

    // Start is called before the first frame update
    void Start()
    {
       // initPlugin(PluginName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initActivity(string activityName)
    {
        unityClass = new AndroidJavaClass("com.ARGames.PhysicsGames");
        unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        _pluginInstance = new AndroidJavaObject(activityName);
        if (_pluginInstance == null)
        {
            Debug.Log("Plugin Instance Error");
        }
        _pluginInstance.CallStatic("receiveActivity", unityActivity);
    }
    void initPlugin(string pluginName)
    {
        unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        _pluginInstance = new AndroidJavaObject(pluginName);
        if (_pluginInstance == null)
        {
            Debug.Log("Plugin Instance Error");
        }
        _pluginInstance.CallStatic("receiveActivity", unityActivity);
    }

    public void Toast()
    {
        if (_pluginInstance != null)
        {
            _pluginInstance.Call("Toast", "Hi, from UNITYYY");
        }
    }
}
