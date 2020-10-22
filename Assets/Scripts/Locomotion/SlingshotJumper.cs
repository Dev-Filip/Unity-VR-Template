using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SlingshotJumper : MonoBehaviour
{

    [Header("SlingshotJump Settings")]

    [Tooltip("How close together the button releases have to be to initiate a jump.")]
    public float releaseWindowTime = 0.5f;
    [Tooltip("Multiplier that increases the jump strength.")]
    public float velocityMultiplier = 5.0f;
    [Tooltip("The maximum velocity a jump can be.")]
    public float velocityMax = 8.0f;


    public Transform playArea;

    [SerializeField] private XRController leftController;
    protected Vector3 leftStartAimPosition;
    protected Vector3 leftReleasePosition;
    protected bool leftIsAiming;

    [SerializeField] private XRController rightController;
    protected Vector3 rightStartAimPosition;
    protected Vector3 rightReleasePosition;
    protected bool rightIsAiming;

    protected bool leftButtonReleased;
    protected bool rightButtonReleased;
    protected float countDownEndTime;

    public Climber climber;
    public VRMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        if(climber== null)
        {
            climber = GetComponent<Climber>();
        }

        if(movement == null)
        {
            movement = GetComponent<VRMovement>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckInput();
    }

    protected void CheckInput()
    {
        if(leftController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton,out bool leftGripPressed))
        {
            if(leftGripPressed)
            {
                LeftButtonPressed();
            }
            else
            {
                LeftButtonReleased();
            }
        }

        if (rightController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool rightGripPressed))
        {
            if (rightGripPressed)
            {
                RightButtonPressed();
            }
            else
            {
                RightButtonReleased();
            }
        }
    }

    protected void LeftButtonPressed()
    {
        // Check for new left aim
        if (!leftIsAiming && !IsClimbing())
        {
            leftIsAiming = true;
            leftStartAimPosition = playArea.InverseTransformPoint(leftController.gameObject.transform.position);
        }
    }
    protected virtual void RightButtonPressed()
    {
        // Check for new right aim
        if (!rightIsAiming && !IsClimbing())
        {
            rightIsAiming = true;
            rightStartAimPosition = playArea.InverseTransformPoint(rightController.gameObject.transform.position);
        }
    }
    private bool IsClimbing()
    {
        return false;
    }

    protected virtual void LeftButtonReleased()
    {
        // Check for release states
        if (leftIsAiming)
        {
            leftReleasePosition = playArea.InverseTransformPoint(leftController.gameObject.transform.position);
            if (!rightButtonReleased)
            {
                countDownEndTime = Time.time + releaseWindowTime;
            }
            leftButtonReleased = true;
        }

        CheckForReset();
        CheckForJump();
    }
    protected virtual void RightButtonReleased()
    {
        // Check for release states
        if (rightIsAiming)
        {
            rightReleasePosition = playArea.InverseTransformPoint(rightController.gameObject.transform.position);
            if (!leftButtonReleased)
            {
                countDownEndTime = Time.time + releaseWindowTime;
            }
            rightButtonReleased = true;
        }

        CheckForReset();
        CheckForJump();
    }

    protected virtual void CancelButtonPressed()
    {
        UnAim();
    }

    protected virtual void CheckForReset()
    {
        if ((leftButtonReleased || rightButtonReleased) && Time.time > countDownEndTime)
        {
            Debug.Log("Slingshot jump reset");
            UnAim();
        }
    }


    protected void UnAim()
    {
        leftIsAiming = false;
        rightIsAiming = false;

        leftButtonReleased = false;
        rightButtonReleased = false;
    }

    protected virtual void CheckForJump()
    {
        if (leftButtonReleased && rightButtonReleased && movement.isGrounded)
        {
            Vector3 leftDir = leftStartAimPosition - leftReleasePosition;
            Vector3 rightDir = rightStartAimPosition - rightReleasePosition;
            Vector3 localJumpDir = leftDir + rightDir;
            Vector3 worldJumpDir = playArea.transform.TransformVector(localJumpDir);
            Vector3 jumpVector = worldJumpDir * velocityMultiplier;

            if (jumpVector.magnitude > velocityMax)
            {
                jumpVector = jumpVector.normalized * velocityMax;
            }

            //movement.characterController.Move(jumpVector);
            //movement.playerVelocity += jumpVector;
            movement.playerVelocity.y = jumpVector.y;
                //Mathf.Sqrt(jumpVector.y*-0.5f*-9.81f);
            //Debug.Log("Player velocity y: " +Mathf.Sqrt(jumpVector.y * -0.5f * -9.81f));
            movement.playerVelocity.x += jumpVector.x;
            //Debug.Log("Player velocity x: " + jumpVector.x);
            movement.playerVelocity.z += jumpVector.z;
           // Debug.Log("Player velocity z: " + jumpVector.z);

            //Debug.Log("Unscaled jump vector: " +jumpVector);
            //Debug.Log("Jump scaled by time.deltatime: " + jumpVector * Time.deltaTime);
            UnAim();

            //OnSlingshotJumped();
        }
    }

    /*
    protected virtual void ApplyBodyMomentum(bool applyMomentum = false)
    {
        if (applyMomentum)
        {
            float rigidBodyMagnitude = bodyRigidbody.velocity.magnitude;
            Vector3 appliedMomentum = playAreaVelocity / (rigidBodyMagnitude < 1f ? 1f : rigidBodyMagnitude);
            bodyRigidbody.AddRelativeForce(appliedMomentum, ForceMode.VelocityChange);
        }
    }
    
     
     
     
     
     
     
     
     
     
     */

}
