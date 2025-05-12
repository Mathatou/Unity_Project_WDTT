using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    /// <summary>
    /// Set the rotation speed of the object.
    /// </summary>
    public float aRotationSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, aRotationSpeed); // Rotate the object around the Y-axis.
        transform.Translate(Vector3.up * Mathf.Sin(Time.time) * 0.0001f); // Move the object up and down.
    }
    

}
