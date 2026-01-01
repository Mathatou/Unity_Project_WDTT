using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInteraction : ObjectInteractionController
{
    public bool isJumpscare = false;
    [SerializeField] private AudioClip harmlessSound;
    [SerializeField] private AudioClip jumpScareSound;
    private void Start()
    {
        harmlessSound.LoadAudioData();
        jumpScareSound.LoadAudioData();
    }
    public override void ObjectInteraction()
    {
        AudioClip clipToPlay = isJumpscare ? jumpScareSound : harmlessSound;
        if (isJumpscare)
        {
            Debug.Log("Jumpscare !");
            // Ajouter ici le code pour le jumpscare (animation, son, etc.)
        }
        else
        {
            Debug.Log("Ghost is harmless.");
        }
        if(clipToPlay != null)
        {
            AudioSource.PlayClipAtPoint(clipToPlay, transform.position);
        }
        Destroy(this.gameObject);
    }
}
