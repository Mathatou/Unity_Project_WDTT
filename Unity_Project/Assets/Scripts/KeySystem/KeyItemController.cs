using System.Collections;
using System.Collections.Generic;
using KeySystem;
using Unity.VisualScripting;
using UnityEngine;

public class KeyItemController : MonoBehaviour
{
    [SerializeField] private bool LockedDoor = false;
    [SerializeField] private bool Key = false;
    [SerializeField] private KeyInventory _keyInventory = null;

    [SerializeField] private GameObject showItemPickedUI = null;
    [SerializeField] private int waitTimer = 1;

    private KeyDoorController doorObject;

    private void Start()
    {
        if(LockedDoor)
        {
            doorObject = GetComponent<KeyDoorController>();
        }

    }
    private IEnumerator ShowItemPicked()
    {
        showItemPickedUI.SetActive( true );
        yield return new WaitForSeconds( waitTimer );
        showItemPickedUI.SetActive( false );
    }

    public void ObjectInteraction()
    {
        if( LockedDoor )
        {
            doorObject.doorOpeningClosing();
        }
        else if (Key)
        {
            _keyInventory.hasKey = true;

            gameObject.SetActive(false);
        }
    }

}
