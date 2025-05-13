using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : ObjectInteractionController
{
    private int fragmentCollected;
    private bool allCollected = false;
    [SerializeField] private NPCInteractionController _Generator;
    [SerializeField] private GameObject showFragmentCollectedUI;
    [SerializeField] private int waitTimer;

    private void Start()
    {
        _Generator = FindAnyObjectByType<NPCInteractionController>();
        Debug.Log("_Generator : " + _Generator.name);
    }
    // Update is called once per frame
    void Update()
    {
        if (AllFragmentsAreCollected() && !allCollected)
        {
            allCollected = true;
            //_Generator.finalGeneration();
            //StartCoroutine(uncoverFinalKeyUI());
            Debug.Log("All fragments collected");
        }
        else if (!allCollected && NPCInteractionController.numberToCollect > 0)
        {
            Debug.Log("Fragment remaining to collect : " + NPCInteractionController.numberToCollect);
        }
    }
    
    private IEnumerator showFragmentUI()
    {
        // Gestion de la récupération de l'objet et de la visibilité du txt
        showFragmentCollectedUI.SetActive(true);
        yield return new WaitForSeconds(waitTimer);
        showFragmentCollectedUI.SetActive(false);
        gameObject.SetActive(false);
        // Désactivation de l'objet et du txt
       
    }
    private bool AllFragmentsAreCollected()
    {
        return fragmentCollected >= NPCInteractionController.numberToCollect;
    }
    public override void ObjectInteraction()
    {
        NPCInteractionController.numberToCollect--;
        StartCoroutine(showFragmentUI());
        Debug.Log($"Fragment number {fragmentCollected} collected");
    }
}
