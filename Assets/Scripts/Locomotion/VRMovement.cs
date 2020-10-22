using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float gravity = -9.81f;
    private float fallingSpeed;
    public ArmSwinger armSwinger;
    public LayerMask groundLayer;
    public GameObject headReference;
    public Vector3 playerVelocity;
    public bool isGrounded;
    public bool isJumping = false;
    public Climber climber;
    // Update is called once per frame
    void FixedUpdate()
    {

        MoveCharacter();
        if (climber.IsClimbing())
        {
            return;
        }
        isGrounded = CheckIfGrounded();

        if (isGrounded)
        {
            if (!(playerVelocity.y > 0f))
            {
                playerVelocity.x = armSwinger.movementDirection.x / Time.deltaTime;
                playerVelocity.z = armSwinger.movementDirection.z / Time.deltaTime;
            }
        }

        characterController.Move(playerVelocity*Time.deltaTime);
        CheckGravity();
    }

    
    private void CheckGravity()
    {
        isGrounded = CheckIfGrounded();
        if (isGrounded)
        {
            //if(playerVelocity.y>0.5f)
            playerVelocity.y = 0f;

        }
        else
        {
            
            playerVelocity.y += gravity * Time.fixedDeltaTime;
        }

        //Gravity
        
    }
    private void MoveCharacter()
    {
        //Get head y position apply it to the character controller
        float headHeight = Mathf.Clamp(headReference.transform.localPosition.y, 1f, 2f);
        characterController.height = headHeight;



        Vector3 newCenter = new Vector3(0f, 0f, 0f);
        //Cut in half to find center height    
        newCenter.y = characterController.height / 2;

        //Get head position on x and z axis apply to the center
        newCenter.x = headReference.transform.localPosition.x;
        newCenter.z = headReference.transform.localPosition.z;

        //Apply final head position to the character controller center
        characterController.center = newCenter;

    }

    bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(characterController.center);
        float rayLength = characterController.height / 2 + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, characterController.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
   
       
        return hasHit;

    }


}