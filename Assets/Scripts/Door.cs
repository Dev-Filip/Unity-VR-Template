using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void CloseDoor()
    {
        animator.SetBool("DoorOpen", false);
    }
    public void OpenDoor()
    {
        animator.SetBool("DoorOpen", true);
    }
    public void PlayDoorShutSound()
    {
        Debug.Log("Door has been shut");
    }
}