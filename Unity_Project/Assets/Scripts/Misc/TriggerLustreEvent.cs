using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLustreEvent : MonoBehaviour
{
    [SerializeField] private GameObject Lustre = null;
    [SerializeField] private GameObject fallenLight = null;
    [SerializeField] private GameObject Light = null;
    [SerializeField] private GameObject fallenLustre= null;
    [SerializeField] private AudioClip fallSound = null;
    [SerializeField] private AudioSource audioSource = null;
    private bool doOnce = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            TriggerLustreFall();
        }
        
    }
    private void TriggerLustreFall()
    {
        if (doOnce) return; // Prevent multiple triggers
        doOnce = true;
        Lustre.SetActive(false);
        fallenLustre.SetActive(true);
        fallenLight.SetActive(true);
        Light.SetActive(false);
        if (audioSource != null && fallSound != null)
        {
            audioSource.PlayOneShot(fallSound);
        }
    }
}
