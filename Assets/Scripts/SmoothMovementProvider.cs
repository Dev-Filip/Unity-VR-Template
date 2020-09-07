
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SmoothMovementProvider : LocomotionProvider
{
    public float speed = 1.0f;
    public List<XRController> controllers = null;

    private CharacterController characterController = null;
    [SerializeField]
    private GameObject head = null;
    protected override void Awake()
    {
        characterController = GetComponent<CharacterController>();
        
    }

    private void Start()
    {
        PositionController();
    }

    private void Update()
    {
        PositionController();
        CheckForInput();
    }

    private void PositionController()
    {
        // Get head height, apply it to the character controller
        float headHeight = Mathf.Clamp(head.transform.localPosition.y, 1, 2);
        characterController.height = headHeight;

        Vector3 newCenter = Vector3.zero;

        // Cut in half to find the center height;
        newCenter.y = characterController.height / 2;

        // Get head position on x and z, apply it to the center.
        newCenter.x = head.transform.localPosition.x;
        newCenter.z = head.transform.localPosition.z;

        // Apply to the character controller
        characterController.center = newCenter;
    }

    private void CheckForInput()
    {

        for(int i = 0; i < controllers.Count;i++)
        {
            if(controllers[i].enableInputActions)
            {
                CheckForMovement(controllers[i].inputDevice);
            }
        }


        foreach (XRController controller in controllers)
        {
            if (controller.enableInputActions)
                CheckForMovement(controller.inputDevice);
        }
    }

    private void CheckForMovement(InputDevice device)
    {
        //Check if controller's touchpad value
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
            StartMove(position);
    }

    private void StartMove(Vector2 position)
    {
        // Apply the touch position to the head's forward Vector
        Vector3 joystickDirection = new Vector3(position.x, 0, position.y);
        Vector3 headRotation = new Vector3(0, head.transform.eulerAngles.y, 0);

        // Rotate the input direction by the horizontal head rotation
        Vector3 direction = Quaternion.Euler(headRotation) * joystickDirection;
        // Apply speed and move
        Vector3 movement = direction * speed;
        characterController.Move(movement * Time.deltaTime);

    }
}