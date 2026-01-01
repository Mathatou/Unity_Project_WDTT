using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInteraction : ObjectInteractionController
{
    public bool isJumpscare = false;
    public override void ObjectInteraction()
    {
        if (isJumpscare)
        {
            Debug.Log("Jumpscare !");
            // Ajouter ici le code pour le jumpscare (animation, son, etc.)
        }
        else
        {
            Debug.Log("Ghost is harmless.");
        }
        Destroy(this.gameObject);
    }
}
