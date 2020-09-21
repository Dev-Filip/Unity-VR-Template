using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEnterRoom : MonoBehaviour
{
    public UnityEvent OnPlayerEnterRoom;
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnPlayerEnterRoom.Invoke();      
        }
    }
    public void LogMessageToConsole(string message)
    {
        Debug.Log(message);
    }
}
