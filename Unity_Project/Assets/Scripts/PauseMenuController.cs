using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI; // Reference to the pause menu UI

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }
    
    public void quitGame()
    {
#if UNITY_EDITOR
        // If we are in the editor, stop playing
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If we are in a build, quit the application
        
        Application.Quit();
#endif
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
}
