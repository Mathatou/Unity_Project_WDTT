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
    /// <summary>
    /// Skip the cinmatic by changing changeTime to 0.
    /// It surely can be done in a better way, but it works in this case ( changing scene ) 
    /// </summary>
    void skipCinematic()
    {
        changeTime = 0;
    }
}
