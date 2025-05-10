using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class NPCInteractionController : ObjectInteractionController
{

    [SerializeField] private AudioClip _clip1 ;
    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private GameObject dialogText;
    // This method will be called when the player interacts with the NPC
    // It will play a sound and log a message to the console
    public override void ObjectInteraction()
    {
        
        if (_AudioSource.isPlaying)
        {
            _AudioSource.Stop();
            _playableDirector.Stop();
            dialogText.SetActive(false);
        }
        else
        {
            _playableDirector.Play();
            _AudioSource.PlayOneShot(_clip1);
        }
        
        Debug.Log("NPC Interaction Triggered");
    }
}
