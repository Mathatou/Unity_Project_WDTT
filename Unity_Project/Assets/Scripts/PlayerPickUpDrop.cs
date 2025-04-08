using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{

    [SerializeField] Transform playerCameraTransform;
    [SerializeField] Transform objectGrabPointTransform;
    [SerializeField] float pickUpDistance = 2.0f;
    
    
    [SerializeField] LayerMask pickupLayerMask;
    private objectGrabbable objectToGrab;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(objectToGrab != null)
            {
                objectToGrab.Drop();
                objectToGrab = null;
                Debug.Log("Item is dropped");
            }
            else
            {
                if (Physics.Raycast(playerCameraTransform.position, 
                                    playerCameraTransform.forward, 
                                    out RaycastHit o_raycastHit, 
                                    pickUpDistance, 
                                    pickupLayerMask)
                   )
                {
                    if (o_raycastHit.transform.TryGetComponent(out objectToGrab))
                    {
                        objectToGrab.Grab(objectGrabPointTransform);
                        Debug.Log("Item is grabbed");
                    }
                }
            }
        }
    }
}
