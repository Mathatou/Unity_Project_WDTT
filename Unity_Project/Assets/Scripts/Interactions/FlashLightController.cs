using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class FlashLightController : ObjectInteractionController
{

    [SerializeField] private Transform eyesPosition;
    [SerializeField] private TextMeshProUGUI tutoFlashLigth;
    private Rigidbody rb;
    private MeshCollider mc;
    private Light lg;
    private bool isHeld = false;
    private ParticleSystem ps;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        mc = this.gameObject.GetComponent<MeshCollider>();
        lg = this.gameObject.GetComponent<Light>();
        ps = this.gameObject.GetComponentInChildren<ParticleSystem>();
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
    private void activateTuto()
    {
        tutoFlashLigth.gameObject.SetActive(true);
    }
    private void deactivateTuto()
    {
        tutoFlashLigth.gameObject.SetActive(false);
    }
    private void grabFlashlight()
    {
        // Deactivate physics components
        deactivateRigidbody();
        deactivateCollider();
        // Parent to the player's eyes position
        this.transform.SetParent(eyesPosition);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        isHeld = true;
        // Deactivate particle system
        ps.gameObject.SetActive(false);
        activateTuto();
    }

    private void activateFlash()
    {
        deactivateTuto();
        lg.enabled = !lg.enabled;
    }
}
