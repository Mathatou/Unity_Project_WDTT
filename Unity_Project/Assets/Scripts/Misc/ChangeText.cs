using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChangeText : MonoBehaviour
{
    [SerializeField] private TMP_Text textObject;
    public void modifyText(string newText)
    {
        if(textObject != null)
        {
            textObject.text = newText;
        }
        else
        {
            Debug.LogError("Text object is not assigned.");
        }
    }
}
