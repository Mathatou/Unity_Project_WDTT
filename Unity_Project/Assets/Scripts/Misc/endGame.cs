using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endGame : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("EndGame");
            /*
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            // Add your game over logic here, such as loading a game over scene or displaying a game over UI.
            // For example:
            // SceneManager.LoadScene("GameOverScene");
            */
        }
    }
}
