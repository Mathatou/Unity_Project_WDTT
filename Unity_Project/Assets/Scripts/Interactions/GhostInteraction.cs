using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInteraction : ObjectInteractionController
{
    public bool isJumpscare = false;
    [SerializeField] private AudioClip harmlessSound;
    [SerializeField] private AudioClip jumpScareSound;
    [SerializeField] private GameObject jumpScare;
    [SerializeField] private float screamerVolume = 0.7f;
    private float jumpsScareDuration = 3f;
    public override void ObjectInteraction()
    {
        AudioClip clipToPlay = isJumpscare ? jumpScareSound : harmlessSound ;
        if (isJumpscare)
        {
            GameObject JS = Instantiate(jumpScare, transform.position, Quaternion.identity);
            Destroy(JS, jumpsScareDuration);
            if(PauseMenuController.instance != null)
            {
                PauseMenuController.instance.StartJumpscareSequence(jumpsScareDuration);
            }
        }
        else
        {
            Debug.Log("Ghost is harmless.");
        }
        if(clipToPlay != null)
        {
            AudioSource.PlayClipAtPoint(clipToPlay, transform.position,screamerVolume);
        }
        Destroy(this.gameObject);
    }
}
