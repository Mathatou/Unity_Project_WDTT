using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MenuController
{
    private void Start()
    {
        Cursor.visible = true; // Ensure the cursor is visible when the main menu is active
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor so it can be moved freely
    }
    /// <summary>
    /// Go from current scene to Histoire scene
    /// </summary>
    public void goHistoire()
    {
        // Load the Histoire scene
        SceneManager.LoadScene("Histoire");
    }
    /// <summary>
    /// Go from current scene to the play scene
    /// </summary>
    public void goJouer()
    {
        SceneManager.LoadScene("Demo_01");
    }
    
}
