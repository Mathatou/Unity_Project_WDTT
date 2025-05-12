using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragInteraction : ObjectInteractionController
{
    public override void ObjectInteraction()
    {
        FragmentGenerator.numberToCollect--;
        Debug.Log($"Number to collect : {FragmentGenerator.numberToCollect}");
    }
}
