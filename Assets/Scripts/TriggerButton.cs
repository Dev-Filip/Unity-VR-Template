using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerButton : XRBaseInteractable
{
    //variables for button's highest and lowest point
    private float heightMin;

    private float heightMax;

    //variable for button's height difference when button is pressed in
    public float pressDepth;

    //variables for UnityEvents that will be fired when button is pressed and released
    public UnityEvent onPress = null;

    public UnityEvent onRelease = null;

    protected override void Awake()
    {
        //call to the baseScript.Awake()

        base.Awake();
        //Configure lowest and highest button point values

        heightMax = transform.localPosition.y;
        heightMin = transform.localPosition.y - pressDepth;
    }

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        //call to baseScript.OnSelectEnter(interactor)

        base.OnSelectEnter(interactor);

        //Move the button down

        SetButtonHeight(heightMin);

        //Call UnityEvent for buttonPressed.
        onPress.Invoke();
    }

    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        //call to baseScript.OnSelectExit(interactor)

        base.OnSelectExit(interactor);

        //Move the button up

        SetButtonHeight(heightMax);

        //Call UnityEvent for buttonReleased.
        onRelease.Invoke();
    }

    private void SetButtonHeight(float height)
    {
        //Create a vector3 variable for new button position

        Vector3 newPosition = transform.localPosition;

        //Change y value of the vector3 (it corresponds to height)
        newPosition.y = height;

        //Pass new position to the button itself.
        transform.localPosition = newPosition;
    }
    public void ConsoleMessage(string message)
    {
        Debug.Log(message);
    }
}