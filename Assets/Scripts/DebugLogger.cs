using UnityEngine;

public class DebugLogger : MonoBehaviour
{
    public void Log(string text) => Debug.Log(text);
    public void LogWarning(string text) => Debug.LogWarning(text);
    public void LogError(string text) => Debug.LogError(text);
}