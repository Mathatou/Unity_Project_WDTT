using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gostmanager : MonoBehaviour
{
    [SerializeField] private GameObject gostPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject theKey;
    [SerializeField] private AudioClip newAmbianceMusic;
    [SerializeField] private GameObject GOambianceSource;
    private AudioSource AS;

    private void Start()
    {
        AS = GOambianceSource.GetComponent<AudioSource>();
        newAmbianceMusic.LoadAudioData();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Désactivation du trigger pour ne pas relancer le spawn
            this.GetComponent<Collider>().enabled = false;
            SpawnGhostsAndHideKey();
            AS.clip = newAmbianceMusic;
            AS.Play();
        }
    }

    void SpawnGhostsAndHideKey()
    {
        // Choix du spot de la clé
        theKey.SetActive(true);
        int keyIndex = Random.Range(0, spawnPoints.Length);
        // Placement de la clé dans la hierarchie 
        theKey.transform.SetParent(this.transform);
        Debug.Log("Key moved to spawn point index: " + keyIndex + " at Position : " + spawnPoints[keyIndex].transform);
        theKey.transform.position = spawnPoints[keyIndex].position;
        theKey.transform.position -= Vector3.up * 0.6f;

        // Choix des spots des jumpscares
        List<int> jumpscareIndices = new List<int>();
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i != keyIndex) 
                availableIndices.Add(i);
        }// On exclut la case clé

        for(int i = 0; i < 5; i++)
        {
            if (availableIndices.Count > 0)
            {
                int rand = Random.Range(0, availableIndices.Count);
                jumpscareIndices.Add(availableIndices[rand]);
                availableIndices.RemoveAt(rand);
            }
        }

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject newGhost = Instantiate(gostPrefab, spawnPoints[i].position, Quaternion.identity);

            // On configure le fantôme (Jumpscare ou Normal ?)
            // Note: On n'a plus besoin de dire "KeyHolder" car la clé est physiquement dessous !
            GhostInteraction ghostScript = newGhost.GetComponent<GhostInteraction>();

            if (jumpscareIndices.Contains(i))
            {
                ghostScript.isJumpscare = true;
            }
        }

    }
}
