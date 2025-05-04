using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private float changeTime;
    [SerializeField] private string sceneName;

    private void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            skipCinematic();
        }
    }
    void skipCinematic()
    {
        changeTime = 0;
    }
}
