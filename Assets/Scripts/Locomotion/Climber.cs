using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Climber : MonoBehaviour
{
    private CharacterController characterController;
    public static XRController climbingHand;
    private VRMovement movementManager;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        movementManager = GetComponent<VRMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(climbingHand)
        {
            movementManager.enabled = false;
            Climb();
        }
        else
        {
            movementManager.enabled = true;
        }
    }
    void Climb()
    {
        InputDevices.GetDeviceAtXRNode(climbingHand.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);

        characterController.Move(transform.rotation*-velocity * Time.fixedDeltaTime);
    }
    public bool IsClimbing()
    {
        return climbingHand != null;
    }
}
