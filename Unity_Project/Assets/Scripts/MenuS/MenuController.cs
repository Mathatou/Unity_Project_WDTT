using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
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
