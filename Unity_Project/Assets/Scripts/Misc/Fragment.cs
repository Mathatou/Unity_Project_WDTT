using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : ObjectInteractionController
{
    private int fragmentCollected;
    private bool allCollected = false;
    [SerializeField] private NPCInteractionController _Generator;
    

    // Update is called once per frame
    void Update()
    {
        if (AllFragmentsAreCollected() && !allCollected)
        {
            allCollected = true;
            _Generator.finalGeneration();
            Debug.Log("All fragments collected");
        }
        else if (!allCollected && NPCInteractionController.numberToCollect > 0)
        {
            Debug.Log("Fragment remaining to collect : " + NPCInteractionController.numberToCollect);
        }
    }
    private bool AllFragmentsAreCollected()
    {
        return fragmentCollected >= NPCInteractionController.numberToCollect;
    }
    public override void ObjectInteraction()
    {
        NPCInteractionController.numberToCollect--;
        gameObject.SetActive(false);
        Debug.Log($"Fragment number {fragmentCollected} collected");
    }
}
