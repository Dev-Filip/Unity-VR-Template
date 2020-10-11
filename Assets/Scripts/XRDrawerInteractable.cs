using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
[RequireComponent(typeof(ConfigurableJoint))]
public class XRDrawerInteractable : XRGrabInteractable
{
    public float threshold = 0.05f;

    public UnityEvent onMinimumLimitHit;
    public UnityEvent onMaximumLimitHit;

    private float minimumPoint = 0f;
    private float maximumPoint = 0f;
    public enum JointState { Minimum, Maximum, InBetween };
    public JointState drawerJointState = JointState.Minimum;
    private ConfigurableJoint configurableJoint = null;

    void Start()
    {
        configurableJoint = GetComponent<ConfigurableJoint>();
        minimumPoint = transform.localPosition.x;
        maximumPoint = transform.localPosition.x + configurableJoint.linearLimit.limit;
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        float distanceToMinLimit = Mathf.Abs(configurableJoint.transform.localPosition.x - minimumPoint);
        float distanceToMaxLimit = Mathf.Abs(configurableJoint.transform.localPosition.x - maximumPoint);

        if (distanceToMinLimit < threshold)
        {
            if (drawerJointState != JointState.Minimum)
            {
                onMinimumLimitHit.Invoke();
            }
            drawerJointState = JointState.Minimum;
        }
        else if (distanceToMaxLimit < threshold)
        {
            if (drawerJointState != JointState.Maximum)
            {
                onMaximumLimitHit.Invoke();
            }
            drawerJointState = JointState.Maximum;
        }
        else
        {
            drawerJointState = JointState.InBetween;
        }
    }
    public void PrintToConsole(string message)
    {
        Debug.Log(message);
    }
}
