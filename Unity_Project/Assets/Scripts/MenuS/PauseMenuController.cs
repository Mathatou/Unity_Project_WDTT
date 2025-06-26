using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MenuController
{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject manualUI;
    [SerializeField] private GameObject RTFM_UI;
    [SerializeField] private AudioClip theme;
    private AudioSource audioSource;
    private bool doWeReadManual = false;

    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = theme;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.volume = 0.25f;
        audioSource.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!doWeReadManual)
            {
                TogglePauseMenu();
                RTFM_UI.SetActive(false);
            }
            else
            {
                BackToMenu();
            }
        }
    }

    public void TogglePauseMenu()
    {
        GameIsPaused = !GameIsPaused;
        pauseMenuUI.SetActive(GameIsPaused);
        Time.timeScale = GameIsPaused ? 0f : 1f;

        if (GameIsPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            audioSource.Pause();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            audioSource.Play();
        }
    }

    public void ReadManual()
    {
        pauseMenuUI.SetActive(false);
        manualUI.SetActive(true);
        doWeReadManual = true;

        // Ensure cursor is unlocked and visible when reading the manual
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void BackToMenu()
    {
        manualUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        doWeReadManual = false;

        // Ensure cursor remains unlocked and visible when returning to the pause menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
