using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class NPCInteractionController : ObjectInteractionController
{

    [SerializeField] private AudioClip _clip1 ;
    [SerializeField] private AudioSource _AudioSource;
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
    [SerializeField] private GameObject finalkey;
    [SerializeField] private GameObject finaltransform;

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
                finalGeneration();
                isSpawned = true;
            }
        }
    }


    public void finalGeneration()
    {
        Debug.Log("Final key generated at : " + finaltransform.transform.position);
        Instantiate(finalkey, finaltransform.transform);
    }
}
