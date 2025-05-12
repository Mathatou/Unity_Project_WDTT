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

    private bool isSpawned = false;
    private int[] randomIndex;
    public static int numberToCollect = 5;
    // Start is called before the first frame update
    public void Start()
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
        if(!isSpawned)
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
