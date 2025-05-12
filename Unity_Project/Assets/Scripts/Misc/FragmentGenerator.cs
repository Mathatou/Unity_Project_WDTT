using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnLocation;
    [SerializeField] private GameObject fragmentToSpawn;
    [Header("Final key and transform")]
    [Space(32)]
    [SerializeField] private GameObject finalkey;
    [SerializeField] private GameObject finaltransform;
    [SerializeField] private int numberToSpawn;

    private int[] randomIndex;
    public static int numberToCollect = 5;
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
            Instantiate(fragmentToSpawn,spawnLocation[randomIndex[i]].transform);
        }
    }
    private void Update()
    {
        if (numberToCollect <= 0)
        {
            Debug.Log("All fragments collected");
            finalGeneration();
        }
    }
    public void finalGeneration()
    {
        Instantiate(finalkey, finaltransform.transform);
    }
}
