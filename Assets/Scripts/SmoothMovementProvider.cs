using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SmoothMovementProvider : LocomotionProvider
{
    //Speed variable
    [SerializeField] private float speed = 1.0f;
    //List of controllers
    public List<XRController> controllers = null;
    //Character controller
    CharacterController characterController = null;
    //Head reference
    [SerializeField] private GameObject head = null;


    protected override void Awake()
    {
        //Call base.Awake
        base.Awake();

        //Setup character controller
        characterController = GetComponent<CharacterController>();
        //Update player position
        PositionUpdate();
    }

    void Update()
    {
        //Update player position
        PositionUpdate();

        //Read input
        CheckForInput();
    }

    void PositionUpdate()
    {
        //Get head height, apply it to the character controller
        float headHeight = head.transform.localPosition.y;
        characterController.height = headHeight;

        //Create variable for center update
        Vector3 newCenter = Vector3.zero;
        //Divide by 2 to get the center height
        newCenter.y = characterController.height / 2;
        //Read head position on X and Z axis, apply it to the center.
        newCenter.x = head.transform.localPosition.x;
        newCenter.z = head.transform.localPosition.z;
        //Apply center to the character controller
        characterController.center = newCenter;
    }
    void CheckForInput()
    {
        //Loop through list of controllers
        foreach (XRController controller in controllers)
        {
            //Check if controller has enabled inputActions
            if (controller.enableInputActions)
            {
                //Check if controller touchpad is being used
                if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
                {
                    StartMove(position);
                }
            }
        }

    }

    void StartMove(Vector2 position)
    {
        //Get touchpad value and player head rotation
        Vector3 joyStickValue = new Vector3(position.x, 0f, position.y);
        Vector3 headRotation = new Vector3(0f, head.transform.eulerAngles.y, 0f);

        //Multiply touchpad input direction with horizontal head rotation
        Vector3 direction = Quaternion.Euler(headRotation) * joyStickValue;

        //Calculate final movement and update character controller
        Vector3 movement = direction * speed;
        characterController.Move(movement * Time.deltaTime);
    }
}
