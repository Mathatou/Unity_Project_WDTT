using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MenuController
{
    public static PauseMenuController instance;
    public static bool GameIsPaused = false;
    private bool isJumpscareActive = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject manualUI;
    [SerializeField] private GameObject RTFM_UI;
    [SerializeField] private AudioClip theme;
    private AudioSource audioSource;
    private bool doWeReadManual = false;
    private bool isCutsceneActive = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
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
        if (Input.GetKeyDown(KeyCode.Escape)&& !isJumpscareActive)
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
            if(!isJumpscareActive && !isCutsceneActive)
                audioSource.Play();
        }
    }
    public void StopMusicForCutscene()
    {
        isCutsceneActive = true; 
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
    public void StartCutsceneMode()
    {
        isCutsceneActive = true; // Bloque la touche Echap

        // 1. On coupe la musique
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        // 2. On s'assure que le menu pause est fermé (sécurité)
        if (GameIsPaused)
        {
            TogglePauseMenu(); // Ferme le menu si le joueur l'avait laissé ouvert
        }

        // 3. On cache le curseur pour l'immersion
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Appelle ça à la fin de la Timeline (si le jeu continue après)
    public void EndCutsceneMode()
    {
        isCutsceneActive = false; // Débloque la touche Echap

        // On relance la musique si besoin
        if (!GameIsPaused && audioSource != null)
        {
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

    public void StartJumpscareSequence(float duration)
    {
        // 1. On bloque le menu pause
        isJumpscareActive = true;

        // 2. On coupe la musique d'ambiance
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }

        // 3. On programme le retour à la normale (Invoke permet d'attendre X secondes)
        Invoke("EndJumpscareSequence", duration);
    }

    private void EndJumpscareSequence()
    {
        // 1. On débloque le menu
        isJumpscareActive = false;

        // 2. On remet la musique (si le jeu n'est pas en pause entre temps)
        if (!GameIsPaused)
        {
            audioSource.Play();
        }
    }


}
