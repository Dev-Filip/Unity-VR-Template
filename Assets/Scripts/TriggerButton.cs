using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerButton : XRBaseInteractable
{
    //Variables for highest and lowest button point
    private float yMax = 0f;
    private float yMin = 0f;

    //Variable for maxDepth
    [SerializeField] private float maxDepth = 0.01f;

    //Variable for Unity Events OnPress/OnRelease
    public UnityEvent OnPress = null;
    public UnityEvent OnRelease = null;

    protected override void Awake()
    {
        //Call base class function
        base.Awake();

        //Configure button highest and lowest point variables
        yMax = transform.localPosition.y;
        yMin = transform.localPosition.y - maxDepth;
    }

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        //Call base class function
        base.OnSelectEnter(interactor);
        //Update button position (it goes down)
        SetYPosition(yMin);
        //Fire event OnPress
        OnPress.Invoke();
    }
    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        //Call base class function
        base.OnSelectExit(interactor);
        //Update button position (it goes up)
        SetYPosition(yMax);
        //Fire event OnRelease
        OnRelease.Invoke();
    }
    private void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = position;
        transform.localPosition = newPosition;
    }
}
