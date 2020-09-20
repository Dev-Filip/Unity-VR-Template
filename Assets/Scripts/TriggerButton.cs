using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerButton : XRBaseInteractable
{
    //Variables for heighest and lowest point

    private float highestPoint = 0f;
    private float lowestPoint = 0f;
    //Variable for maximum button depression

    [SerializeField] private float maxDepress = 0.01f;
    //Variables for Unity events OnPress/OnRelease

    public UnityEvent OnPress = null;
    public UnityEvent OnRelease = null;

    protected override void Awake()
    {
        //Call its baseclass
        base.Awake();

        //Set button height maximum and minimum values
        lowestPoint = transform.localPosition.y - maxDepress;
        highestPoint = transform.localPosition.y;
    }
    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        //call baseclass
        base.OnSelectEnter(interactor);

        //Set button height lowest point
        SetHeight(lowestPoint);

        //Invoke OnPress
        OnPress.Invoke();
    }
    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        //call baseclass
        base.OnSelectExit(interactor);

        //Set button height maximum point
        SetHeight(highestPoint);


        //Invoke OnRelease
        OnRelease.Invoke();
    }
    private void SetHeight(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = position;
        transform.localPosition = newPosition;
    }
    public void ConsoleLog(string text)
    {
        Debug.Log(text);
    }
}
