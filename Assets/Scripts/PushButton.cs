using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class PushButton : XRBaseInteractable
{

    private float yMin = 0.0f;
    private float yMax = 0.0f;
    private bool previousPress = false;

    private float previousHandHeight = 0.0f;
    private XRBaseInteractor hoverInteractor = null;

    public float maxDepth = 0.1f;

    public UnityEvent OnPress = null;
    public UnityEvent OnRelease = null;


    protected override void Awake()
    {
        base.Awake();
        
        //Set y min and max
        yMin = transform.localPosition.y - maxDepth;
        yMax = transform.localPosition.y;
    }

    protected override void OnHoverEnter(XRBaseInteractor interactor)
    {
        base.OnHoverEnter(interactor);


        hoverInteractor = interactor;
        previousHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
    }

    protected override void OnHoverExit(XRBaseInteractor interactor)
    {
        base.OnHoverExit(interactor);

        hoverInteractor = null;
        previousHandHeight = 0.0f;

        previousPress = false;
        SetYPosition(yMax);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {

        if(hoverInteractor)
        {
            float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
            float handDifference = previousHandHeight - newHandHeight;
            previousHandHeight = newHandHeight;

            float newPosition = transform.localPosition.y - handDifference;
            SetYPosition(newPosition);

            CheckPress();
        }
        
        base.ProcessInteractable(updatePhase);
    }

    private float GetLocalYPosition(Vector3 position)
    {
        Vector3 localPosition = transform.parent.InverseTransformPoint(position);

        return localPosition.y;
    }

    private void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Clamp(position, yMin, yMax);

        transform.localPosition = newPosition;
    }

    private void CheckPress()
    {
        //check if near the bottom
        bool inPosition = transform.localPosition.y <= yMin + 0.01f;

        if (inPosition != previousPress)
        {
            if (inPosition)
                OnPress.Invoke();
            else
                OnRelease.Invoke();
        }

        previousPress = inPosition;
    }
}
