using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    /// <summary> Variable describing the time before changing scene, the easiest way is to set changeTime to the duration of the cutscene</summary>
    [SerializeField] private float changeTime;
    /// <summary> Name of the scene we want to change to </summary>
    [SerializeField] private string sceneName;
    /// <summary> PlayableDirector to play the cutscene </summary>
    [SerializeField] private PlayableDirector cutscene;

    [SerializeField] private bool isLastCutscene = false;


    private void Start()
    {
        cutscene.stopped += OnCinematicEnd;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            skipCinematic();
        }
    }
    /// <summary>
    /// Skip the cinmatic stopping it and loading the next scene
    /// </summary>
    void skipCinematic()
    {
        quitLastCinematic();
        // Arrête la timeline et charge la scène suivante
        cutscene.Stop();
        LoadNextScene();
    }
    void OnCinematicEnd(PlayableDirector director)
    {
        quitLastCinematic();
        LoadNextScene();
    }
    void LoadNextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    void quitLastCinematic()
    {
        if (isLastCutscene)
        {
            MenuController.QuitGame();
        }
    }
}
