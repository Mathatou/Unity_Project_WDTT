using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
/// <summary>
/// Classe utilisée pour changer de scène après un certain temps (le temps qu'une cinématique se termine).
/// </summary>
public class ChangeScene : MonoBehaviour
{
    [SerializeField] private float changeTime;
    [SerializeField] private string sceneName;
    [SerializeField] private PlayableDirector cutscene;
    [SerializeField] private bool isLastCutscene = false;

    private void Start()
    {
        // Start the coroutine to wait for the specified changeTime
        StartCoroutine(WaitAndLoadScene());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            skipCinematic();
        }
    }

    void skipCinematic()
    {
        if (isLastCinematic())
        {
            MenuController.QuitGame();
        }
        // Stop the cutscene and load the next scene
        cutscene.Stop();
        LoadNextScene();
    }

    IEnumerator WaitAndLoadScene()
    {
        // Wait for the specified changeTime
        yield return new WaitForSeconds(changeTime);

        if (isLastCinematic())
        {
            MenuController.QuitGame();
        }
        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    bool isLastCinematic()
    {
        return isLastCutscene;
    }
}
