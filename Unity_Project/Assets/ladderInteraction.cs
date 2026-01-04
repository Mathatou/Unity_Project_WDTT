using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTutorial.PlayerControl;

public class ladderInteraction : ObjectInteractionController
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    public override void ObjectInteraction()
    {
        Debug.Log("Flash Interaction");
        teleport();
    }
    void teleport()
    {
        Transform playerTransform = PlayerController.Instance.gameObject.transform;
        float midHeight = (pointA.position.y + pointB.position.y) / 2.0f;

        if (playerTransform.position.y < midHeight)
        {
            // Je suis en bas (< milieu) -> Je vais en HAUT
            playerTransform.position = pointB.position;
            playerTransform.rotation = pointB.rotation; // On s'oriente vers la sortie
        }
        else
        {
            // Je suis en haut (> milieu) -> Je vais en BAS
            playerTransform.position = pointA.position;
            playerTransform.rotation = pointA.rotation;
        }

    }
}
