using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorInteraction : ObjectInteractionController
{
    [SerializeField] GameObject mirrorUI;
    [SerializeField] private int waitTimer = 1;

    public override void ObjectInteraction()
    {
        Debug.Log("Mirror Interaction Triggered");
        StartCoroutine(ShowMirrorInteraction());
    }
    private IEnumerator ShowMirrorInteraction()
    {
        mirrorUI.SetActive(true);
        yield return new WaitForSeconds(waitTimer);
        mirrorUI.SetActive(false);
    }
}
