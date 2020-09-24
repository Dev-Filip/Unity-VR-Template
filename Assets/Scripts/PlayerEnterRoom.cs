using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEnterRoom : MonoBehaviour
{
    public UnityEvent OnPlayerEnter = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnPlayerEnter.Invoke();
            Destroy(this.gameObject);
        }
    }
    public void LogToConsole(string text)
    {
        Debug.Log(text);
    }
}
