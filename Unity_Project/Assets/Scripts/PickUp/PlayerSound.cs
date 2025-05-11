using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private FootstepController footstepController;

    private void Awake()
    {
        footstepController = GetComponentInChildren<FootstepController>(); // Get the FootstepController component
    }

    private void Update()
    {
        // Player movement code here

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
}