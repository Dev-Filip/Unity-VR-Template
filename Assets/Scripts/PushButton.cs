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

        onHoverEnter.AddListener(StartPress);
        onHoverExit.AddListener(EndPress);

        SetMinMax();
    }

    private void OnDestroy()
    {
        onHoverEnter.AddListener(StartPress);
        onHoverExit.AddListener(EndPress);
    }

    private void StartPress(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
        previousHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
    }

    private void EndPress(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
        previousHandHeight = 0.0f;

        previousPress = false;
        SetYPosition(yMax);
    }

    private void SetMinMax()
    {
        Collider collider = GetComponent<Collider>();
        yMin = transform.localPosition.y - maxDepth;
        yMax = transform.localPosition.y;
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
        bool inPosition = InPosition();

        if (inPosition != previousPress)
        {
            if (inPosition)
                OnPress.Invoke();
            else
                OnRelease.Invoke();
        }

        previousPress = inPosition;
    }

    private bool InPosition()
    {
        //float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMin + 0.01f);
        //return transform.localPosition.y == inRange;

        return transform.localPosition.y <= yMin + 0.01f;
    }
}
