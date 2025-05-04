using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KeyRaycast : MonoBehaviour
{
    // Distance the raycast will check for objects
    [SerializeField] private int rayLength = 5;

    // Layer mask for interactable objects
    [SerializeField] private LayerMask InteractionLayerMask;

    // Optional: layer name to exclude from raycast (can be null)
    [SerializeField] private string excluseLayerName = null;

    // Reference to the currently raycasted interactable object
    private ObjectInteractionController _raycastedObject;

    // Key used to trigger the interaction (default: left mouse click)
    [SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;

    // Tracks if the crosshair (or interaction state) is active
    private bool isCrossHairActive;

    // Ensures the same object isn’t repeatedly selected each frame
    private bool doOnce;

    // Tag used to identify interactable objects
    private string interactableTag = "InteractiveObject";

    private void Update()
    {
        // Store hit info from raycast
        RaycastHit hit;

        // Cast ray forward from the object’s transform
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        // Combine layer mask: includes the interaction mask and excludes a specified layer
        int mask = (1 << LayerMask.NameToLayer(excluseLayerName)) | InteractionLayerMask.value;

        // Perform the raycast
        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            // Check if the hit object has the correct tag
            if (hit.collider.CompareTag(interactableTag))
            {
                // Only get component once per object
                if (!doOnce)
                {
                    _raycastedObject = hit.collider.gameObject.GetComponent<ObjectInteractionController>();
                }

                isCrossHairActive = true;
                doOnce = true;

                // If the interaction key is pressed, perform the interaction
                if (Input.GetKeyDown(openDoorKey))
                {
                    _raycastedObject.ObjectInteraction();
                }
            }
        }
        else
        {
            // Reset flags if raycast does not hit a valid object
            if (isCrossHairActive)
            {
                doOnce = false;
            }
        }
    }
}
