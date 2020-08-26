using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class SelectButton : XRBaseInteractable
{

    private float yMin = 0.0f;
    private float yMax = 0.0f;

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

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);

        hoverInteractor = interactor;
        SetYPosition(yMin);

        OnPress.Invoke();
    }

    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);

        hoverInteractor = null;
        previousHandHeight = 0.0f;

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
