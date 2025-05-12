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
    [SerializeField] private GameObject spawnlocation;
    private bool IsAlreadyClicked = false;
    // This method will be called when the player interacts with the NPC
    // It will play a sound and log a message to the console

    [SerializeField] private GameObject[] spawnLocation;
    [SerializeField] private GameObject fragmentToSpawn;
    [SerializeField] private int numberToSpawn;
    private int[] randomIndex;
    // Start is called before the first frame update
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

    public override void ObjectInteraction()
    {
        if (!IsAlreadyClicked)
        {
            IsAlreadyClicked = true;
        }
        
        if (_playableDirector.state == PlayState.Playing)
        {
            _AudioSource.Stop();
            _playableDirector.Stop();
            _playableDirector.time = 0;
        }
        else
        {
            _playableDirector.Play();
            _AudioSource.PlayOneShot(_clip1);
        }
        
        Debug.Log("NPC Interaction Triggered");
    }
}
