using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : ObjectInteractionController
{
    private int fragmentCollected;
    [SerializeField] private NPCInteractionController _Generator;
    private bool allCollected = false;

    // Update is called once per frame
    void Update()
    {
        if (AllFragmentsAreCollected() && !allCollected)
        {
            allCollected = true;
            Debug.Log("All fragments collected");
        }
        else if (!allCollected && FragmentGenerator.numberToCollect > 0)
        {
            Debug.Log("Fragment remaining to collect : " + FragmentGenerator.numberToCollect);
        }
    }
    private bool AllFragmentsAreCollected()
    {
        return fragmentCollected >= FragmentGenerator.numberToCollect;
    }
    public override void ObjectInteraction()
    {
        FragmentGenerator.numberToCollect--;
        Debug.Log($"Fragment number {fragmentCollected} collected");
    }
}
