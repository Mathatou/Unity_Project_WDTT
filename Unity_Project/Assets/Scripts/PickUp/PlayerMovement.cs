using UnityEngine;

public class PlayerMovement : MonoBehaviour
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

        if (isWalking)
        {
            footstepController.StartWalking();
        }
        else
        {
            footstepController.StopWalking();
        }
    }
}