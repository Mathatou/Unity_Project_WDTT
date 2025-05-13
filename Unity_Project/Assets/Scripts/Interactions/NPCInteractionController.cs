using KeySystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [Space(32)]
    [SerializeField] private GameObject[] spawnLocation;
    [SerializeField] private GameObject fragmentToSpawn;
    [SerializeField] private int numberToSpawn;
    [Header("Final phase of the game Settings")]
    [Space(32)]

    [SerializeField] private GameObject showFinalKeyUI = null;
    [SerializeField] private GameObject finalkey;
    [SerializeField] private GameObject finaltransform;
    [SerializeField] private float waitTimer = 5.0f;
    
    public static int numberToCollect = 5;
    private int[] randomIndex;
    private bool isSpawned = false;

    /// <summary>
    /// Is called when clicked on
    /// </summary>
    public override void ObjectInteraction()
    {
        if (_playableDirector.state == PlayState.Playing)
        {
            _playableDirector.Stop();
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
