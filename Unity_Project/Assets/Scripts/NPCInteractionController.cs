using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractionController : ObjectInteractionController
{
  
    // Update is called once per frame
    void Update()
    {
     
    }
    public override void ObjectInteraction()
    {
        Debug.Log("NPC Interaction Triggered");
    }
}
