using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : ObjectInteractionController
{
    private int fragmentCollected;
<<<<<<< Updated upstream
    [SerializeField] private NPCInteractionController _Generator;

=======
>>>>>>> Stashed changes
    // Update is called once per frame
    void Update()
    {
        if (AllFragmentsAreCollected())
        {
            Debug.Log("All fragments collected");
        }
        else
        {
            Debug.Log("Fragment remaining to collect : " + FragmentGenerator.numberToCollect);
        }
    }
    private bool AllFragmentsAreCollected()
    {
<<<<<<< Updated upstream
        return fragmentCollected == NPCInteractionController.numberToCollect;
=======
        return fragmentCollected >= FragmentGenerator.numberToCollect;
>>>>>>> Stashed changes
    }
    public override void ObjectInteraction()
    {
        FragmentGenerator.numberToCollect--;
        Debug.Log($"Fragment number {fragmentCollected} collected");
    }
}
