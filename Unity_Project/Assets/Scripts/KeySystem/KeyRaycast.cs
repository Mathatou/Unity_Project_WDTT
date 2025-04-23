using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KeyRaycast : MonoBehaviour
{
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask InteractionLayerMask;
    [SerializeField] private string excluseLayerName = null;

    private KeyItemController _raycastedObject;
    [SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;
    [SerializeField] private Image crosshair = null;
    private bool isCrossHairActive;
    private bool doOnce;

    private string interactableTag = "InteractiveObject";

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = (1 << LayerMask.NameToLayer(excluseLayerName)) | InteractionLayerMask.value;

        if(Physics.Raycast(transform.position,fwd,out hit, rayLength, mask ) )
        {
            if(hit.collider.CompareTag(interactableTag))
            {
                if(!doOnce)
                {
                    _raycastedObject = hit.collider.gameObject.GetComponent<KeyItemController>();
                    CrosshairChange( true );
                }
                isCrossHairActive = true;
                doOnce = true;

                if(Input.GetKeyDown(openDoorKey))
                {
                    _raycastedObject.ObjectInteraction();
                }
            }
                
        }
        else
        {
            if(isCrossHairActive)
            {
                CrosshairChange(false );
                doOnce = false;
            }
        }
    }
    private void CrosshairChange( bool on )
    {
        if(on && doOnce)
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = Color.white;
            isCrossHairActive = false;
        }
    }
}
