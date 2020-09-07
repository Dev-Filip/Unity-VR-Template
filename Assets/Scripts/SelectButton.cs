using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class SelectButton : XRBaseInteractable
{

    private float yMin = 0.0f;
    private float yMax = 0.0f;


    public float maxDepth = 0.1f;

    public UnityEvent OnPress = null;
    public UnityEvent OnRelease = null;


    protected override void Awake()
    {
        base.Awake();

        //Set button heigh maximum and minimum values
        yMin = transform.localPosition.y - maxDepth;
        yMax = transform.localPosition.y;
    }

    //Called when we begin interaction with the button
    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);

        SetYPosition(yMin);

        OnPress.Invoke();
    }

    //Called when we finish interaction with the button
    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);

        SetYPosition(yMax);

        OnRelease.Invoke();
    }

    private void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = position;

        transform.localPosition = newPosition;
    }
}
