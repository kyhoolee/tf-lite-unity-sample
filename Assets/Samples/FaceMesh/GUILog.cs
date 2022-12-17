using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUILog : MonoBehaviour
{
    string myLog;
    List<string> myLogQueue = new List<string>();

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
        print("---------------GUI Log Start----------------");
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
        print("---------------GUI Log End----------------");
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if(type == LogType.Log || type == LogType.Error)
        {
            if (myLogQueue.Count > 20)
            {
                myLogQueue.RemoveAt(0);
            }
            myLogQueue.Add(logString);
            myLog = "";
            foreach (string log in myLogQueue)
            {
                myLog += log + "\n";
            }
        }
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 120);
        style.alignment = TextAnchor.UpperRight;
        style.fontSize = h * 2 / 120;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);

        GUI.Label(rect, myLog, style);
    }
}
