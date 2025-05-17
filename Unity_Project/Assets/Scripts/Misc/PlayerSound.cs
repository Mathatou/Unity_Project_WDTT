using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private FootstepController footstepController;
    private bool warned = false; // Flag to track if the warning has been logged

    private void Awake()
    {
        footstepController = GetComponentInChildren<FootstepController>(); // Get the FootstepController component
    }

    private void Update()
    {
        if(footstepController == null && !warned)
        {
            Debug.LogError("FootstepController not found on the player or children of player.");
            warned = true; // Set the flag to true to avoid logging again
            return;
        }
        // Player movement code here
        if (!warned)
        {
            // Check if the player is walking or not and call the appropriate methods
            bool isWalking = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
            bool isRunning = Input.GetKey(KeyCode.LeftShift) && isWalking;
            if (isRunning)
            {
                footstepController.StartRunning();
            }
            else if (isWalking)
            {
                footstepController.StartWalking();
            }
            else
            {
                footstepController.StopWalking();
            }
        }
        else
        {
            return;
        }
    }
        
}