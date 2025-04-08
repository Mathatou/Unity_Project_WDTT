using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{

    [SerializeField] Transform playerCameraTransform;
    [SerializeField] float pickUpDistance = 2.0f;
    [SerializeField] LayerMask pickupLayerMask;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit o_raycastHit, pickUpDistance, pickupLayerMask))
            {
                Debug.Log(o_raycastHit.transform);
            }
        }
    }
}
