using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endCarInteraction : ObjectInteractionController
{
    public override void ObjectInteraction()
    {
        Debug.Log("End Car Interaction triggered, loading CutSceneFINALE");
        SceneManager.LoadScene("CutSceneFINALE");
    }
}
