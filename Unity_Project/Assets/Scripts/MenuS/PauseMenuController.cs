using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MenuController
{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI; // Reference to the pause menu UI
    [SerializeField] private GameObject manualUI; // Reference to the manual UI
    [SerializeField] private GameObject RTFM_UI; // Reference to the text indicating to read the manual 
    [SerializeField] private AudioClip theme; // Glisser l'audio ici dans l'inspecteur
    private AudioSource audioSource;
    private bool doWeReadManual = false;

    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = theme;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 0 = 2D, 1 = 3D
        audioSource.volume = 0.6f;

        audioSource.Play();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!doWeReadManual)
            {
                TogglePauseMenu();
                RTFM_UI.SetActive(false); // Hide the "Read the manual" text when the pause menu is opened
            }
            else
            {
                BackToMenu();
            }
        }
    }
    /// <summary>
    /// This method allows to display or hide the pause menu
    /// </summary>
    public void TogglePauseMenu()
    {
        GameIsPaused = !GameIsPaused; // Switch the game state
        pauseMenuUI.SetActive(GameIsPaused); // Show / Hide the pause menu UI
        Time.timeScale = GameIsPaused ? 0f : 1f; // Freeze / Unfreeze the game
        
        if (GameIsPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            audioSource.Pause();
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            audioSource.Play();
            Cursor.visible = false;
        }
    }
    /// <summary>
    /// Show / Hide the manualUI in the pause menu
    /// </summary>
    public void ReadManual()
    {
        pauseMenuUI.SetActive(false); // Show / Hide the pause menu UI
        doWeReadManual = true; // Switch the game state
        manualUI.SetActive(true); // Show / Hide the manual UI
    }
    /// <summary>
    /// Go back from Manual menu to Pause menu
    /// </summary>
    public void BackToMenu()
    {
        pauseMenuUI.SetActive(true); // Show / Hide the pause menu UI
        manualUI.SetActive(false); // Show / Hide the manual UI
    }

}
