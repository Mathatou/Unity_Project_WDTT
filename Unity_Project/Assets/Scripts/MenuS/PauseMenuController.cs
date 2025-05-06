using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MenuController
{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI; // Reference to the pause menu UI
    [SerializeField] private GameObject manualUI; // Reference to the manual UI
    [SerializeField] private GameObject RTFM_UI; // Reference to the text indicating to read the manual 
    private bool isInManualMode = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isInManualMode)
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
    public void TogglePauseMenu()
    {
        GameIsPaused = !GameIsPaused; // Switch the game state
        pauseMenuUI.SetActive(GameIsPaused); // Show / Hide the pause menu UI
        Time.timeScale = GameIsPaused ? 0f : 1f; // Freeze / Unfreeze the game
        
        if (GameIsPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ToggleManual()
    {
        pauseMenuUI.SetActive(false); // Show / Hide the pause menu UI
        isInManualMode = true; // Switch the game state
        manualUI.SetActive(true); // Show / Hide the manual UI
    }
    public void BackToMenu()
    {
        pauseMenuUI.SetActive(true); // Show / Hide the pause menu UI
        manualUI.SetActive(false); // Show / Hide the manual UI
    }

}
