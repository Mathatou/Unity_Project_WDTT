using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Animator doorAnimator;


    void Start()
    {
        // Get the Animator component attached to the same GameObject as this script
        doorAnimator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player (or another specified object)
        Debug.Log("Trigger entered is triggered");
        if (other.CompareTag("Player")) // Make sure the player GameObject has the tag "Player"
        {
            if (doorAnimator != null)
            {
                Debug.Log("Player has entered the trigger zone. Triggering door animation.");
                // Trigger the Door_Open animation
                doorAnimator.SetTrigger("Door_Opening");
            }
        }
    }
}
