using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;
    [SerializeField]private AudioClip openDoor;
    [SerializeField]private AudioClip closeDoor;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        
        audioSource.PlayOneShot(closeDoor);
        Debug.Log("Door has been shut");
    }
}
