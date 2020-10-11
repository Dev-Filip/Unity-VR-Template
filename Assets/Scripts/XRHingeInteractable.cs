using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
[RequireComponent(typeof(HingeJoint))]
public class XRHingeInteractable : XRGrabInteractable
{
    public float threshold = 1f;

    public UnityEvent onMinimumLimitHit;
    public UnityEvent onMaximumLimitHit;

    public enum HingeJointState { Minimum,Maximum,InBetween};
    public HingeJointState hingeJointState = HingeJointState.InBetween;
    private HingeJoint hinge = null;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        float angleToMinLimit = Mathf.Abs(hinge.angle - hinge.limits.min);
        float angleToMaxLimit = Mathf.Abs(hinge.angle - hinge.limits.max);

        if(angleToMinLimit<threshold)
        {
            if(hingeJointState != HingeJointState.Minimum)
            {
                onMinimumLimitHit.Invoke();
            }
            hingeJointState = HingeJointState.Minimum;
        }
        else if(angleToMaxLimit<threshold)
        {
            if (hingeJointState != HingeJointState.Maximum)
            {
                onMaximumLimitHit.Invoke();
            }
            hingeJointState = HingeJointState.Maximum;
        }
        else
        {
            hingeJointState = HingeJointState.InBetween;
        }
    }
    public void PrintToConsole(string message)
    {
        Debug.Log(message);
    }


}
