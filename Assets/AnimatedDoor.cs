using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedDoor : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void CloseDoor()
    {
        animator.SetBool("IsDoorOpen",false);
    }
    
    public void OpenDoor()
    {
        animator.SetBool("IsDoorOpen", true);
    }
    public void PlayDoorShutSound()
    {
        Debug.Log("Door has been shut");
    }

   
}
