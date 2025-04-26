using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectGrabbable : MonoBehaviour
{
    private Rigidbody m_objectRigidBody;
    private Transform m_ObjectGrabPointTransform;
    private float lerpSpeed = 5.0f;
    private void Awake()
    {
        m_objectRigidBody = GetComponent<Rigidbody>();
    }

    public void Grab(Transform ObjectGrabPointTransform)
    {
        this.m_ObjectGrabPointTransform = ObjectGrabPointTransform;
        m_objectRigidBody.useGravity = false;
    }
    public void Drop()
    {
        this.m_ObjectGrabPointTransform = null;
        this.m_objectRigidBody.useGravity = true;
    }

    private void FixedUpdate()
    {
        if(m_ObjectGrabPointTransform!=null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, m_ObjectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            m_objectRigidBody.MovePosition(newPosition);
        }
        
    }
}
