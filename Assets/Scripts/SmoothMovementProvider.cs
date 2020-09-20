using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SmoothMovementProvider : LocomotionProvider
{
    //Variable for speed
    [SerializeField] float speed = 1.0f;
    //Variable for a controller
    public XRController controller = null;

    //Variable for character controller
    private CharacterController characterController = null;
    //Variable for player's head
    [SerializeField] private GameObject head = null;
    private bool isSprinting = false;
    [SerializeField] private float sprintMultiplier = 2.0f;

    protected override void Awake()
    {
        base.Awake();
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        //Updating player's current position
        PositionUpdate();
        //Reading input from the controller
        CheckForInput();
    }
    private void PositionUpdate()
    {
        //Get head height, apply it to the character controller
        float headHeight = Mathf.Clamp(head.transform.localPosition.y, 1f, 2f);
        characterController.height = headHeight;

        Vector3 newCenter = Vector3.zero;
        //Cut in half to find the center height
        newCenter.y = characterController.height / 2;

        //Get head position on x and z axis, apply to the center
        newCenter.x = head.transform.localPosition.x;
        newCenter.z = head.transform.localPosition.z;

        //Apply to the character controller
        characterController.center = newCenter;
    }
    private void CheckForInput()
    {
        if(controller.enableInputActions)
        {
            if(controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
            {
                controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out isSprinting);
                StartMove(position);
            }
        }
    }
    private void StartMove(Vector2 position)
    {
        //Apply the touch position to the head's forward vector
        Vector3 joystickDirection = new Vector3(position.x, 0f, position.y);
        Vector3 headRotation = new Vector3(0f, head.transform.eulerAngles.y, 0f);

        //Rotate the input direction by the horizontal head rotation
        Vector3 direction = Quaternion.Euler(headRotation) * joystickDirection;

        //Apply speed and command character controller to move
        Vector3 movement = direction * speed;
        if (isSprinting)
        {
            movement = movement * sprintMultiplier;
        }
        
        characterController.Move(movement*Time.deltaTime);
    }
}
