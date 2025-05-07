using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class to other menu scripts
/// </summary>
public class MenuController : MonoBehaviour
{
    /// <summary>
    /// Quit game wether we are in editor or build mode
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        // If we are in the editor, stop playing
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If we are in a build, quit the application
        
        Application.Quit();
#endif
    }
}
