using KeySystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class NPCInteractionController : ObjectInteractionController
{
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private GameObject dialogText;
    private bool IsAlreadyClicked = false;
    // This method will be called when the player interacts with the NPC
    // It will play a sound and log a message to the console
    [Header("Spawn Fragment Settings")]
    [Space(16)]
    [SerializeField] private Transform parentSpawnLocation;
    [SerializeField] private GameObject[] spawnLocation;
    [SerializeField] private GameObject fragmentToSpawn;
    [SerializeField] private int numberToSpawn;
    [Header("Final phase of the game Settings")]
    [Space(16)]

    [SerializeField] private GameObject showFinalKeyUI = null;
    [SerializeField] private GameObject finalkey;
    [SerializeField] private GameObject finaltransform;
    [SerializeField] private float waitTimer = 5.0f;
    
    [SerializeField] public int numbernecessary= 5;
    public static int numberToCollect = 5;
    private int[] randomIndex;
    private bool isSpawned = false;

    [ContextMenu("Remplir la liste Auto")] // Crée l'option dans le menu
    void AutoFillSpawnPoints()
    {
        // 1. Détermine qui est le parent (soit une variable, soit l'objet lui-même)
        Transform targetParent = parentSpawnLocation != null ? parentSpawnLocation : transform;

        // 2. Crée une liste temporaire (plus facile à manipuler qu'un array)
        List<GameObject> tempPoints = new List<GameObject>();

        // 3. Boucle sur chaque enfant du parent
        foreach (Transform child in targetParent)
        {
            // On évite de s'ajouter soi-même si le script est sur le parent
            if (child != transform)
            {
                tempPoints.Add(child.gameObject);
            }
        }

        // 4. Convertit la liste en tableau pour ta variable
        spawnLocation = tempPoints.ToArray();

        Debug.Log($"C'est fait ! {spawnLocation.Length} points de spawn trouvés.");
    }

    private void Awake()
    {
        numberToCollect = numbernecessary;
        isSpawned = false;
    }


    /// <summary>
    /// Is called when clicked on
    /// </summary>
    public override void ObjectInteraction()
    {
        if (_playableDirector.state == PlayState.Playing)
        {
            _playableDirector.time = 0;
        }
        else
        {
            _playableDirector.Play();
        }
        // Prevents the apparition of the fragments multiple times 
        if (!IsAlreadyClicked)
        {
            Debug.Log("Dialog Triggered");
            IsAlreadyClicked = true;
            SpawnFragments();
        }
        Debug.Log("NPC Interaction Triggered");
    }
    private IEnumerator ShowLastKeyPicked()
    {
        // Gestion de la récupération de l'objet et de la visibilité du txt
        showFinalKeyUI.SetActive(true);
        Debug.Log("Texte apparait pendant " + waitTimer + "sec.");
        yield return new WaitForSeconds(waitTimer);
        // Désactivation de l'objet et du txt
        showFinalKeyUI.SetActive(false);
    }

    public void SpawnFragments()
    {
        randomIndex = new int[numberToSpawn];
        for (int i = 0; i < numberToSpawn; i++)
        {
            randomIndex[i] = Random.Range(0, spawnLocation.Length);
            for (int j = 0; j < i; j++)
            {
                if (randomIndex[i] == randomIndex[j])
                {
                    randomIndex[i] = Random.Range(0, spawnLocation.Length);
                    j = -1; // Restart the loop to check for duplicates again
                }
            }
        }
        for (int i = 0; i < numberToSpawn; i++)
        {
            Debug.Log($"Item spawned at : {spawnLocation[randomIndex[i]].transform.position}");
            Instantiate(fragmentToSpawn, spawnLocation[randomIndex[i]].transform);
        }
    }
    private void Update()
    {
        if (!isSpawned)
        {
            if (numberToCollect <= 0)
            {
                Debug.Log("All fragments collected");
                StartCoroutine(finalGeneration());
                isSpawned = true;
            }
        }
    }

    public IEnumerator finalGeneration()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Final key generated at : " + finaltransform.transform.position);
        StartCoroutine(ShowLastKeyPicked());
        Instantiate(finalkey, finaltransform.transform);
    }
}
