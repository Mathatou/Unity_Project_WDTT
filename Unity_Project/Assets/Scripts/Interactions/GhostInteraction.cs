using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInteraction : ObjectInteractionController
{
    public bool isJumpscare = false;
    [SerializeField] private AudioClip harmlessSound;
    [SerializeField] private AudioClip[] jumpScareSounds;
    [SerializeField] private GameObject jumpScare;
    public override void ObjectInteraction()
    {
        AudioClip clipToPlay;
        if (isJumpscare)
        {
            int randIndex = Random.Range(0, jumpScareSounds.Length);
            clipToPlay = jumpScareSounds[randIndex];
            GameObject JS = Instantiate(jumpScare, transform.position, Quaternion.identity);
            Destroy(JS, 3f);
            Debug.Log("Jumpscare !");
        }
        else
        {
            clipToPlay = harmlessSound;
            Debug.Log("Ghost is harmless.");
        }
        if(clipToPlay != null)
        {
            AudioSource.PlayClipAtPoint(clipToPlay, transform.position);
        }
        Destroy(this.gameObject);
    }
}
