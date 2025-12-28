using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLightController : ObjectInteractionController
{
    [SerializeField] private Transform eyesPosition;
    private Rigidbody rb;
    private MeshCollider mc;
    private Light lg;
    private bool isHeld = false;
    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        mc = this.gameObject.GetComponent<MeshCollider>();
        lg = this.gameObject.GetComponent<Light>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isHeld)
        {
            activateFlash();
        }
    }
    public override void ObjectInteraction()
    {
        Debug.Log("Flash Interaction");
        grabFlashlight();
    }
    private void deactivateCollider()
    {
        mc.enabled = false;
    }
    private void deactivateRigidbody()
    {
        rb.isKinematic=true;
        rb.interpolation = RigidbodyInterpolation.None;
    }

    private void grabFlashlight()
    {
        deactivateRigidbody();
        deactivateCollider();
        this.transform.SetParent(eyesPosition);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        isHeld = true;
    }

    private void activateFlash()
    {
        lg.enabled = !lg.enabled;
    }
}
