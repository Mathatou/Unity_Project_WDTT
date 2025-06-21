using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace KeySystem
{
    public class KeyDoorController : MonoBehaviour
    {
        private Animator doorAnim;
        private HingeJoint Joint;
        private Rigidbody doorRb;
        private AudioSource audioSource;


        /*[Header( "Animation Names" )]
        [SerializeField] private string openAnimationName = "DoorOpen";
        [SerializeField] private string closeAnimationName = "DoorClose";*/

        [SerializeField] private GameObject showDoorLockedUI = null;


        [SerializeField] private KeyInventory _inventory = null;

        [SerializeField] private int waitTimer = 1;
        private bool doOnce = false;

        private void Awake()
        {
            doorAnim = GetComponent<Animator>();
            Joint = GetComponent<HingeJoint>();
            doorRb = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
            doorRb.isKinematic = true; // Make the door kinematic to prevent physics interactions
            Joint.useLimits = true;
            Joint.limits = new JointLimits { min = 0, max = 0 }; // Door can't move initially
            Joint.enableCollision = true;
        }
        private IEnumerator ShowDoorLocked()
        {
            showDoorLockedUI.SetActive( true );
            yield return new WaitForSeconds(waitTimer);
            showDoorLockedUI.SetActive( false );
        }

        public void doorOpeningClosing()
        {
            if( _inventory.hasKey )
            {
                if(!doOnce)
                {
                    Debug.Log("Opening Door");
                    Joint.useLimits = false;
                    Joint.limits = new JointLimits { min = -90, max = 0 }; // Door can move to open position
                    Joint.enableCollision = false;
                    doorRb.isKinematic = false; // To allow physics interactions
                    Debug.Log("Playing sound door open");
                    audioSource.PlayOneShot(audioSource.clip); // Play door opening sound
                }
                doOnce = true; // Prevent further opening until closed  
            }
            else
            {
                StartCoroutine( ShowDoorLocked() );
            }
        }
    }
}