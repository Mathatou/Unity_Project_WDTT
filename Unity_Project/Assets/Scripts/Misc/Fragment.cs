using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : ObjectInteractionController
{
    private int fragmentCollected;
    [SerializeField] private NPCInteractionController _Generator;

    // Update is called once per frame
    void Update()
    {
        if (AllFragmentsAreCollected())
        {
            Debug.Log("All fragments collected");
            _Generator.finalGeneration();
        }
        else
        {
            Debug.Log("Fragments collected: " + fragmentCollected);
        }
    }
    private bool AllFragmentsAreCollected()
    {
        return fragmentCollected == NPCInteractionController.numberToCollect;
    }
    public override void ObjectInteraction()
    {

        base.ObjectInteraction();
    }
}
