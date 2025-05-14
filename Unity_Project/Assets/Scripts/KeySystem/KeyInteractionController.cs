using System.Collections;
using System.Collections.Generic;
using KeySystem;
using Unity.VisualScripting;
using UnityEngine;

public class KeyInteractionController : ObjectInteractionController
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
    /// <summary>
    /// Cette fonction va afficher un texte lorsque le joueur ramasse un objet.
    /// Il va attendre que le texte parte (1s) avant de le cacher (le récuperer).
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowItemPickedAndDeactivates()
    {
        // Gestion de la récupération de l'objet et de la visibilité du txt
        _keyInventory.hasKey = true;
        showItemPickedUI.SetActive( true );
        yield return new WaitForSeconds( waitTimer );
        // Désactivation de l'objet et du txt
        showItemPickedUI.SetActive( false );
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 
    /// </summary>
    public override void ObjectInteraction()
    {
        if ( LockedDoor )
        {
            doorObject.doorOpeningClosing();
        }
        else if (Key)
        {
            StartCoroutine(ShowItemPickedAndDeactivates());
        }
    }

}
