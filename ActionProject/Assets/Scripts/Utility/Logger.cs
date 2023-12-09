using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ENV_DEV

public static class Logger
{
    public static void Log(string msg)
    {
        Debug.Log(msg);
    }

    public static void LogWarning(string msg)
    {
        Debug.LogWarning(msg);
    }

    public static void LogError(string msg)
    {
        Debug.LogError(msg);
    }
}
#endif