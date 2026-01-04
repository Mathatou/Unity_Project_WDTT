using UnityEngine;

public class cutsceneRelay : MonoBehaviour
{
    // La Timeline va appeler cette fonction
    public void MuteMusicAndBlockPause()
    {
        // Cette fonction appelle ton Singleton qui flotte dans le "DontDestroyOnLoad"
        if (PauseMenuController.instance != null)
        {
            PauseMenuController.instance.StartCutsceneMode();
            Debug.Log("Relay : Mode Cinématique activé (Musique OFF, Echap bloqué)");
        }
    }

    // Optionnel : Si tu veux réactiver à la fin
    public void RestoreMusicAndPause()
    {
        if (PauseMenuController.instance != null)
        {
            PauseMenuController.instance.EndCutsceneMode();
        }
    }
}