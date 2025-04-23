using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace KeySystem
{
    public class KeyDoorController : MonoBehaviour
    {
        private Animator doorAnim;
        private bool doorOpen = false;

        [Header( "Animation Names" )]
        [SerializeField] private string openAnimationName = "DoorOpen";
        [SerializeField] private string closeAnimationName = "DoorClose";

        [SerializeField] private int timeToShowUI = 1;
        [SerializeField] private GameObject showDoorLockedUI = null;


        [SerializeField] private KeyInventory _inventory = null;

        [SerializeField] private int waitTimer = 1;
        [SerializeField] private bool pauseInteraction = false;


        private void Awake()
        {
            doorAnim = GetComponent<Animator>();
        }

        private IEnumerator PauseDoorInteraction()
        {
            pauseInteraction = true;
            yield return new WaitForSeconds(waitTimer);
            pauseInteraction = false;
        }
        
        private IEnumerator ShowDoorLocked()
        {
            showDoorLockedUI.SetActive( true );
            yield return new WaitForSeconds(waitTimer);
            showDoorLockedUI.SetActive( false );
        }

        private void playOpenOrCloseAnim(string animName )
        {
            doorAnim.Play( animName, 0, 0.0f );
            doorOpen = false;
            StartCoroutine( PauseDoorInteraction() );
        }
        public void doorOpeningClosing()
        {
            if( _inventory.hasKey )
            {
                if( !doorOpen && !pauseInteraction )
                {
                    playOpenOrCloseAnim( openAnimationName );
                }
                else if( doorOpen && !pauseInteraction )
                {
                    playOpenOrCloseAnim( closeAnimationName );
                }
            }
            else
            {
                StartCoroutine( ShowDoorLocked() );
            }
        }





    }
}