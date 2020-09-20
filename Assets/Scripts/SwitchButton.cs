using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
public class SwitchButton : XRBaseInteractable
{
    //Variables for heighest and lowest point

    private float highestPoint = 0f;
    private float lowestPoint = 0f;
    //Variable for maximum button depression

    [SerializeField] private float maxDepress = 0.01f;
    //Variables for Unity events OnPress/OnRelease

    public UnityEvent OnToggleOn = null;
    public UnityEvent OnToggleOff = null;

    private bool buttonState = false;

    protected override void Awake()
    {
        //Call its baseclass
        base.Awake();
        
        //Set button height maximum and minimum values
        lowestPoint = transform.localPosition.y - maxDepress;
        highestPoint = transform.localPosition.y;

        //Reset button height
        SetHeight(highestPoint);
    }
    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {

        //call baseclass
        base.OnSelectEnter(interactor);

        buttonState = !buttonState;

        if(buttonState)
        {
            //Set button height lowest point
            SetHeight(lowestPoint);
            OnToggleOn.Invoke();
        }
        else
        {
            //Set button height lowest point
            SetHeight(highestPoint);
            OnToggleOff.Invoke();
        }
    

       
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
    public void ChangeColorBlue()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
    public void ChangeMColorRed()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
