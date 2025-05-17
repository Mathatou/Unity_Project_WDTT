using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public AudioClip[] footstepSounds; // Array to hold footstep sound clips
    public float minTimeBetweenFootsteps = 0.3f; // Minimum time between footstep sounds
    public float maxTimeBetweenFootsteps = 0.6f; // Maximum time between footstep sounds
    public float minTimeBetweenRunningSteps = 0.4f; // Minimum time between running footstep sounds
    public float maxTimeBetweenRunningSteps = 0.4f; // Maximum time between running footstep sounds

    private AudioSource audioSource; // Reference to the Audio Source component
    private bool isWalking = false; // Flag to track if the player is walking
    private bool isRunning = false; // Flag to track if the player is running
    private float timeSinceLastFootstep; // Time since the last footstep sound

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); // Get the Audio Source component
    }

    private void Update()
    {
        // Check if the player is walking or running
        if (isWalking || isRunning)
        {
            // Determine the time range based on whether the player is walking or running
            float minTime = isRunning ? minTimeBetweenRunningSteps : minTimeBetweenFootsteps;
            float maxTime = isRunning ? maxTimeBetweenRunningSteps : maxTimeBetweenFootsteps;
            //if (isRunning)
            //{
            //    Debug.Log($"Running --- minTime : {minTime} --- maxTime : {maxTime}");
            //}
            //else
            //{
            //    Debug.Log($"Walking --- minTime : {minTime} --- maxTime : {maxTime}");
            //}
            // Check if enough time has passed to play the next footstep sound
            if (Time.time - timeSinceLastFootstep >= Random.Range(minTime, maxTime))
            {
                // Play a random footstep sound from the array
                AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];
                audioSource.PlayOneShot(footstepSound);

                timeSinceLastFootstep = Time.time; // Update the time since the last footstep sound
            }
        }
    }

    // Call this method when the player starts walking
    public void StartWalking()
    {
        isWalking = true;
        isRunning = false;
    }

    // Call this method when the player starts running
    public void StartRunning()
    {
        isRunning = true;
        isWalking = false;
    }

    // Call this method when the player stops walking or running
    public void StopWalking()
    {
        isWalking = false;
        isRunning = false;
    }
}
