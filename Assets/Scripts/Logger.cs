using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Logger
{
    public static bool IsLogging = false;


    [MenuItem("Logs/Do Logging")]
    static void ToggleLogs()
    {
        var status = IsLogging;
        
        if (status == false)
            IsLogging = true;
        else
            IsLogging = false;


        if (status == false)
            Menu.SetChecked("Logs/Do Logging", false);
        else
            Menu.SetChecked("Logs/Do Logging", true);

    }


    public static void Log(string msg)
    {
        if (IsLogging)
        {
            Debug.Log(msg);
        }
    }
}
