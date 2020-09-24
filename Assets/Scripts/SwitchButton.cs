using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class SwitchButton : XRBaseInteractable
{
    //Variables for highest and lowest button point
    private float yMax = 0f;

    private float yMin = 0f;

    //Variable for maxDepth
    [SerializeField] private float maxDepth = 0.01f;

    public UnityEvent OnToggleOn = null;
    public UnityEvent OnToggleOff = null;

    private bool switchState = false;

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

        switchState = !switchState;

        if (switchState)
        {
            SetYPosition(yMin);
            OnToggleOn.Invoke();
            
        }
        else
        {
            SetYPosition(yMax);
            OnToggleOff.Invoke();
        }
    }

    private void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = position;
        transform.localPosition = newPosition;
    }

    public void ChangeColor()
    {
        if (switchState)
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else if (!switchState)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}