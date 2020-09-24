using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SmoothMovementProvider : LocomotionProvider
{
    //Variable for speed
    [SerializeField] private float speed = 1.0f;

    //Variable for the controller
    public XRController controller = null;

    //Variable for character controller
    private CharacterController characterController;

    //Variable for head
    public GameObject head;

    public float sprintMultiplier = 2f;
    protected override void Awake()
    {
        //Call to base function
        base.Awake();
        //Configure character controller
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //Move the character controller
        MoveCharacter();
        //Read the input
        CheckForInput();
    }

    private void MoveCharacter()
    {
        //Get head y position apply it to the character controller
        float headHeight = Mathf.Clamp(head.transform.localPosition.y, 1f, 2f);
        characterController.height = headHeight;

        Vector3 newCenter = new Vector3(0f, 0f, 0f);
        //Cut in half to find center height
        newCenter.y = characterController.height / 2;

        //Get head position on x and z axis apply to the center
        newCenter.x = head.transform.localPosition.x;
        newCenter.z = head.transform.localPosition.z;

        //Apply final head position to the character controller center
        characterController.center = newCenter;
    }

    private void CheckForInput()
    {
        if (controller.enableInputActions)
        {
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
            {
                if(controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool isSprinting))
                { 
                    StartMove(position,isSprinting);
                }
                
            }
        }
    }

    private void StartMove(Vector2 analogPosition, bool isSprinting)
    {
        //Convert analog position from Vector2 to Vector3
        Vector3 joystickDirection = new Vector3(analogPosition.x, 0f, analogPosition.y);
        //Convert head rotation into vector3
        Vector3 headRotation = new Vector3(0f, head.transform.eulerAngles.y, 0f);
        //Compute a common direction from head and analog input
        Vector3 direction = Quaternion.Euler(headRotation) * joystickDirection;
        //Multiply computed common direction with speed
        Vector3 movement = direction * speed;

        
        if (isSprinting)
            movement = movement * sprintMultiplier;
        
        //Apply it to the character controller
        characterController.Move(movement * Time.deltaTime);
    }
}