using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MenuController
{
    public void goHistoire()
    {
        // Load the Histoire scene
        SceneManager.LoadScene("Histoire");
    }
    public void goJouer()
    {
        SceneManager.LoadScene("Demo_01");
    }
    
}
